using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using SaveUp.Interfaces;
using SaveUp.Models;
using SaveUp.Services;

namespace SaveUp.ViewModels
{
    public class ListPageViewModel : INotifyPropertyChanged
    {
        private static ListPageViewModel _instance;
        public static ListPageViewModel Instance => _instance ??= new ListPageViewModel(new SavedMoneyServiceAPI(new ConfigurationBuilder().Build()));

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        private decimal _gesamtGespart;
        public string GesamtGespartText => $"Gesamt gespart: {_gesamtGespart:0.00} CHF";

        public ObservableCollection<Artikel> ArtikelListe { get; set; } = new ObservableCollection<Artikel>();

        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand MoreCommand { get; }
        public ICommand DeleteCommand { get; }

        public ListPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;

            HomeCommand = new Command(async () => await OnHome());
            ListCommand = new Command(async () => await OnList());
            AddCommand = new Command(async () => await OnAdd());
            MoreCommand = new Command(async () => await OnMore());
            DeleteCommand = new Command<Artikel>(OnDelete);

            // Load data initially
            LoadArtikel();
        }

        private async void LoadArtikel()
        {
            var result = await _savedMoneyService.GetAllAsync();
            if (result.IsSuccess)
            {
                var items = await result.ParseSuccess();
                ArtikelListe.Clear();
                foreach (var item in items)
                {
                    ArtikelListe.Add(new Artikel { Beschreibung = item.Description, Datum = item.Date, Preis = item.Price });
                }
                UpdateGesamtGespart();
                SortArtikel();
            }
            else
            {
                // Handle error
            }
        }

        private void SortArtikel()
        {
            var sorted = ArtikelListe.OrderByDescending(a => a.Datum).ToList();
            ArtikelListe.Clear();
            foreach (var artikel in sorted)
            {
                ArtikelListe.Add(artikel);
            }
        }

        private void UpdateGesamtGespart()
        {
            _gesamtGespart = ArtikelListe.Sum(a => a.Preis);
            OnPropertyChanged(nameof(GesamtGespartText));
        }

        private void OnDelete(Artikel artikel)
        {
            ArtikelListe.Remove(artikel);
            UpdateGesamtGespart();
        }

        private async Task OnHome()
        {
            await Shell.Current.GoToAsync("//home");
        }

        private async Task OnList()
        {
            await Shell.Current.GoToAsync("//list");
        }

        private async Task OnAdd()
        {
            await Shell.Current.GoToAsync("//add");
        }

        private async Task OnMore()
        {
            await Shell.Current.GoToAsync("//more");
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
