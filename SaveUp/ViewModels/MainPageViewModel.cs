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

            _gesamtGespart = 12.00m;
            _heuteGespart = 12.00m;

            OnPropertyChanged(nameof(GesamtGespartText));
            OnPropertyChanged(nameof(HeuteGespartBetragText));
        }

        private void OnHome()
        {
            // Navigation logic for Home
        }

        private void OnList()
        {
            // Navigation logic for List
        }

        private void OnAdd()
        {
            // Navigation logic for Add
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
