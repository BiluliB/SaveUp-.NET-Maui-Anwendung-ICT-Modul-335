using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveUp.Interfaces;
using SaveUp.Services;
using SaveUpModels.Models;

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
                    OnPropertyChanged(nameof(IsNoEntriesMessageVisible));
                }
            }
        }

        public bool IsErrorVisible => !string.IsNullOrEmpty(ErrorMessage) && ErrorMessage != "Keine Einträge für heute.";
        public bool IsNoEntriesMessageVisible => ErrorMessage == "Keine Einträge für heute.";

        public ObservableCollection<SavedMoney> EinsparungenHeute { get; set; } = new ObservableCollection<SavedMoney>();

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        public ICommand RefreshCommand { get; }

        public MainPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;
            RefreshCommand = new Command(async () => await OnRefresh());
        }

        public async Task LoadEinsparungen()
        {
            try
            {
                EinsparungenHeute.Clear();
                _gesamtGespart = 0;
                _heuteGespart = 0;
                ErrorMessage = string.Empty;

                var content = await _savedMoneyService.GetTodayAsync();
                if (content.IsSuccess)
                {
                    var parsed = await content.ParseSuccess();

                    foreach (var item in parsed)
                    {
                        EinsparungenHeute.Add(new SavedMoney { Description = item.Description, Price = item.Price, Date = item.Date.ToLocalTime() }); // Konvertieren in lokale Zeit
                        _gesamtGespart += item.Price;
                        _heuteGespart += item.Price;
                    }

                    if (parsed.Count == 0)
                    {
                        ErrorMessage = "Keine Einträge für heute.";
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
            OnPropertyChanged(nameof(IsErrorVisible));
            OnPropertyChanged(nameof(IsNoEntriesMessageVisible));
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
}
