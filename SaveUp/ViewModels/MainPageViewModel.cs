using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
<<<<<<< HEAD
using SaveUp.Interfaces;
using SaveUp.Models;
using SaveUp.Services;
using SaveUpModels.DTOs.Responses;
=======
using Microsoft.Maui.Controls;
using SaveUp.Interfaces;
<<<<<<< Updated upstream
=======
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes

namespace SaveUp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    {
=======
>>>>>>> Stashed changes
    {        

>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD

        public ICommand RefreshCommand { get; }

        public MainPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;
=======
>>>>>>> Stashed changes

        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;

            DeleteCommand = new Command<Einsparung>(OnDelete);
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86

            RefreshCommand = new Command(async () => await OnRefresh());

            // Load data initially
            LoadInitialData();
        }

<<<<<<< Updated upstream
        private async Task LoadEinsparungen()
        {
            var content = await _savedMoneyService.GetAllAsync();
            if (content.IsSuccess)
            {
                var parsed = await content.ParseSuccess();

=======
<<<<<<< HEAD
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
=======
        private async Task LoadEinsparungen()
        {
            var content = await _savedMoneyService.GetAllAsync();
            if (content.IsSuccess)
            {
                var parsed = await content.ParseSuccess();

>>>>>>> Stashed changes
                foreach (var item in parsed)
                {
                    EinsparungenHeute.Add(new Einsparung { Beschreibung = item.Description, Price = $"{item.Price:F2}" });
                    _gesamtGespart += item.Price;
                    _heuteGespart += item.Price;
                }
            }
<<<<<<< Updated upstream
=======
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes

            OnPropertyChanged(nameof(GesamtGespartText));
            OnPropertyChanged(nameof(HeuteGespartBetragText));
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
        private async Task OnRefresh()
        {
            IsRefreshing = true;

            await LoadEinsparungen();

            IsRefreshing = false;
=======
>>>>>>> Stashed changes
        private void OnDelete(Einsparung einsparung)
        {
            if (EinsparungenHeute.Contains(einsparung))
            {
                EinsparungenHeute.Remove(einsparung);
                _heuteGespart -= decimal.Parse(einsparung.Beschreibung.Split(':')[1].Trim().Replace(" CHF", ""));
                _gesamtGespart -= decimal.Parse(einsparung.Beschreibung.Split(':')[1].Trim().Replace(" CHF", ""));
                OnPropertyChanged(nameof(GesamtGespartText));
                OnPropertyChanged(nameof(HeuteGespartBetragText));
            }
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
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
