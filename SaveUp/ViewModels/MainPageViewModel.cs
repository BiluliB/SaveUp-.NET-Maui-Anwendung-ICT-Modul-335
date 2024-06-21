using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SaveUp.Interfaces;

namespace SaveUp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {        

        private decimal _gesamtGespart;
        public string GesamtGespartText => $"Gesamt gespart: {_gesamtGespart:0.00} CHF";

        private decimal _heuteGespart;
        public string HeuteGespartBetragText => $"{_heuteGespart:0.00} CHF";

        private bool _isHomePage;
        public bool IsHomePage
        {
            get => _isHomePage;
            set
            {
                if (_isHomePage != value)
                {
                    _isHomePage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HomeIconSource));
                }
            }
        }

        private bool _isListPage;
        public bool IsListPage
        {
            get => _isListPage;
            set
            {
                if (_isListPage != value)
                {
                    _isListPage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ListIconSource));
                }
            }
        }

        private bool _isAddPage;
        public bool IsAddPage
        {
            get => _isAddPage;
            set
            {
                if (_isAddPage != value)
                {
                    _isAddPage = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMorePage;
        public bool IsMorePage
        {
            get => _isMorePage;
            set
            {
                if (_isMorePage != value)
                {
                    _isMorePage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MoreIconSource));
                }
            }
        }

        public string HomeIconSource => IsHomePage ? "Resources/Images/home_filled.png" : "Resources/Images/home.png";
        public string ListIconSource => IsListPage ? "Resources/Images/list_filled.png" : "Resources/Images/list.png";
        public string AddIconSource => IsAddPage ? "Resources/Images/add_filled.png" : "Resources/Images/add.png";
        public string MoreIconSource => IsMorePage ? "Resources/Images/more.png" : "Resources/Images/more.png";

        public ObservableCollection<Einsparung> EinsparungenHeute { get; set; } = new ObservableCollection<Einsparung>();

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;

            DeleteCommand = new Command<Einsparung>(OnDelete);

            // Set initial state
            IsHomePage = true; // Initially highlight the Home button

            // Simulate data loading
            LoadEinsparungen();
        }

        private async Task LoadEinsparungen()
        {
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

            OnPropertyChanged(nameof(GesamtGespartText));
            OnPropertyChanged(nameof(HeuteGespartBetragText));
        }

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
