using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SaveUp.ViewModels
{
    public class ListPageViewModel : INotifyPropertyChanged
    {
        private static ListPageViewModel _instance;
        public static ListPageViewModel Instance => _instance ??= new ListPageViewModel();

        private decimal _gesamtGespart;
        public string GesamtGespartText => $"Gesamt gespart: {_gesamtGespart:0.00} CHF";

        public ObservableCollection<Artikel> ArtikelListe { get; set; } = new ObservableCollection<Artikel>();

        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand MoreCommand { get; }

        public ListPageViewModel()
        {
            HomeCommand = new Command(OnHome);
            ListCommand = new Command(OnList);
            AddCommand = new Command(OnAdd);
            MoreCommand = new Command(OnMore);

            // Simulate data loading
            LoadArtikel();
        }

        private void LoadArtikel()
        {
            // Hier sollten die Daten aus der Datenbank geladen werden
            ArtikelListe.Add(new Artikel { Beschreibung = "Kaffee", Datum = new DateTime(2024, 1, 1), Preis = 4.50m });
            ArtikelListe.Add(new Artikel { Beschreibung = "Snack", Datum = new DateTime(2024, 3, 1), Preis = 2.00m });
            ArtikelListe.Add(new Artikel { Beschreibung = "Schoggi", Datum = new DateTime(2024, 5, 1), Preis = 3.00m });
            ArtikelListe.Add(new Artikel { Beschreibung = "RedBull", Datum = new DateTime(2024, 5, 1), Preis = 2.50m });

            _gesamtGespart = 12.00m;

            OnPropertyChanged(nameof(GesamtGespartText));
        }

        private void OnHome()
        {
            // Navigation logic for Home
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void OnList()
        {
            // Navigation logic for List
        }

        private void OnAdd()
        {
            // Navigation logic for Add
            Application.Current.MainPage.Navigation.PushAsync(new Views.AddPage());
        }

        private void OnMore()
        {
            // Navigation logic for More
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Artikel
    {
        public string Beschreibung { get; set; }
        public DateTime Datum { get; set; }
        public decimal Preis { get; set; }
    }
}
