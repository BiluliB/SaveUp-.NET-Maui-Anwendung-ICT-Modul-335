using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveUp.Interfaces;
using SaveUp.Models;
using SaveUp.Services;
using SaveUpModels.DTOs.Responses;

namespace SaveUp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private decimal _gesamtGespart;
        public string GesamtGespartText => $"Gesamt gespart: {_gesamtGespart:0.00} CHF";

        private decimal _heuteGespart;
        public string HeuteGespartBetragText => $"{_heuteGespart:0.00} CHF";

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsErrorVisible));
                }
            }
        }

        public bool IsErrorVisible => !string.IsNullOrEmpty(ErrorMessage);

        public ObservableCollection<Einsparung> EinsparungenHeute { get; set; } = new ObservableCollection<Einsparung>();

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        public ICommand RefreshCommand { get; }

        public MainPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;

            RefreshCommand = new Command(async () => await OnRefresh());

            // Load data initially
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            await LoadEinsparungen();
        }

        public async Task LoadEinsparungen()
        {
            try
            {
                EinsparungenHeute.Clear();
                _gesamtGespart = 0;
                _heuteGespart = 0;
                ErrorMessage = string.Empty;

                var content = await _savedMoneyService.GetAllAsync();
                if (content.IsSuccess)
                {
                    var parsed = await content.ParseSuccess();

                    foreach (var item in parsed)
                    {
                        EinsparungenHeute.Add(new Einsparung { Beschreibung = item.Description, Price = $"{item.Price:F2}" });
                        _gesamtGespart += item.Price;
                        _heuteGespart += item.Price;
                    }
                }
                else
                {
                    ErrorMessage = "Fehler beim Laden der Daten.";
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Fehler beim Laden der Daten.";
            }

            OnPropertyChanged(nameof(GesamtGespartText));
            OnPropertyChanged(nameof(HeuteGespartBetragText));
        }

        private async Task OnRefresh()
        {
            IsRefreshing = true;

            await LoadEinsparungen();

            IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Einsparung
    {
        public string Beschreibung { get; set; }
        public string Price { get; set; }
    }
}
