using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SaveUp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private static MainPageViewModel _instance;
        public static MainPageViewModel Instance => _instance ??= new MainPageViewModel();

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

        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainPageViewModel()
        {
            HomeCommand = new Command(OnHome);
            ListCommand = new Command(OnList);
            AddCommand = new Command(OnAdd);
            DeleteCommand = new Command<Einsparung>(OnDelete);

            // Set initial state
            IsHomePage = true; // Initially highlight the Home button

            // Simulate data loading
            LoadEinsparungen();
        }

        private void LoadEinsparungen()
        {
            // Hier sollten die Daten aus der Datenbank geladen werden
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Kaffee: 4.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Snack: 2.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Schoggi: 3.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "RedBull: 2.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Kaffee: 4.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Snack: 2.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Schoggi: 3.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "RedBull: 2.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Kaffee: 4.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Snack: 2.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Schoggi: 3.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "RedBull: 2.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Kaffee: 4.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Snack: 2.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Schoggi: 3.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "RedBull: 2.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Kaffee: 4.50 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Snack: 2.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "Schoggi: 3.00 CHF" });
            EinsparungenHeute.Add(new Einsparung { Beschreibung = "RedBull: 2.50 CHF" });


            _gesamtGespart = 12.00m;
            _heuteGespart = 12.00m;

            OnPropertyChanged(nameof(GesamtGespartText));
            OnPropertyChanged(nameof(HeuteGespartBetragText));
        }

        private void OnHome()
        {
            IsHomePage = true;
            IsListPage = false;
            IsAddPage = false;
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void OnList()
        {
            IsHomePage = false;
            IsListPage = true;
            IsAddPage = false;
            Application.Current.MainPage.Navigation.PushAsync(new SaveUp.Views.ListPage());
        }

        private void OnAdd()
        {
            IsHomePage = false;
            IsListPage = false;
            IsAddPage = true;
            Application.Current.MainPage.Navigation.PushAsync(new SaveUp.Views.AddPage());
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
    }
}
