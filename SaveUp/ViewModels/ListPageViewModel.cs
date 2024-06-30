﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using SaveUp.Interfaces;
using SaveUp.Services;
using SaveUpModels.Models;

namespace SaveUp.ViewModels
{
    public class ListPageViewModel : INotifyPropertyChanged
    {
        private static ListPageViewModel _instance;
        public static ListPageViewModel Instance => _instance ??= new ListPageViewModel(new SavedMoneyServiceAPI(new ConfigurationBuilder().Build()));

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        private decimal _gesamtGespart;
        public string GesamtGespartText => $"Gesamt gespart: {_gesamtGespart:0.00} CHF";

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

        public bool IsErrorVisible => !string.IsNullOrEmpty(ErrorMessage) && ErrorMessage != "Keine Einträge vorhanden.";
        public bool IsNoEntriesMessageVisible => ErrorMessage == "Keine Einträge vorhanden.";

        public ObservableCollection<SavedMoney> ArtikelListe { get; set; } = new ObservableCollection<SavedMoney>();

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }

        public ListPageViewModel(ISavedMoneyServiceAPI savedMoneyService)
        {
            _savedMoneyService = savedMoneyService;
            RefreshCommand = new Command(async () => await OnRefresh());
            DeleteCommand = new Command<SavedMoney>(OnDelete);
        }

        public async Task LoadArtikel()
        {
            try
            {
                ArtikelListe.Clear();
                _gesamtGespart = 0;
                ErrorMessage = string.Empty;

                var result = await _savedMoneyService.GetAllAsync();
                if (result.IsSuccess)
                {
                    var items = await result.ParseSuccess();
                    foreach (var item in items)
                    {
                        ArtikelListe.Add(new SavedMoney { Description = item.Description, Date = item.Date, Price = item.Price });
                    }

                    if (ArtikelListe.Count == 0)
                    {
                        ErrorMessage = "Keine Einträge vorhanden.";
                    }

                    UpdateGesamtGespart();
                    SortArtikel();
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
            OnPropertyChanged(nameof(IsErrorVisible));
            OnPropertyChanged(nameof(IsNoEntriesMessageVisible));
        }

        private async Task OnRefresh()
        {
            if (IsRefreshing) return;

            IsRefreshing = true;
            await LoadArtikel();
            IsRefreshing = false;
        }

        private void SortArtikel()
        {
            var sorted = ArtikelListe.OrderByDescending(a => a.Date).ToList();
            ArtikelListe.Clear();
            foreach (var artikel in sorted)
            {
                ArtikelListe.Add(artikel);
            }
        }

        private void UpdateGesamtGespart()
        {
            _gesamtGespart = ArtikelListe.Sum(a => a.Price);
            OnPropertyChanged(nameof(GesamtGespartText));
        }

        private void OnDelete(SavedMoney artikel)
        {
            ArtikelListe.Remove(artikel);
            UpdateGesamtGespart();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
