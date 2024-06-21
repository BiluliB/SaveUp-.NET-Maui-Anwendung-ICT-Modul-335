using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using SaveUp.Interfaces;
using SaveUp.Services;
using SaveUpModels.DTOs.Requests;

namespace SaveUp.ViewModels
{
    public class AddPageViewModel : INotifyPropertyChanged
    {
        private static AddPageViewModel _instance;
        public static AddPageViewModel Instance => _instance ??= new AddPageViewModel();

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _kurzbeschreibung;
        public string Kurzbeschreibung
        {
            get => _kurzbeschreibung;
            set
            {
                if (_kurzbeschreibung != value)
                {
                    _kurzbeschreibung = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _preis;
        public string Preis
        {
            get => _preis;
            set
            {
                if (_preis != value)
                {
                    _preis = value;
                    OnPropertyChanged();
                }
            }
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNotBusy));
                }
            }
        }

        public bool IsNotBusy => !IsBusy;

=======
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        public AddPageViewModel()
        {
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
            AddCommand = new Command(async () => await ExecuteAddCommandAsync(), () => IsNotBusy);
=======
>>>>>>> Stashed changes
            AddCommand = new Command(OnAdd);
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
            CancelCommand = new Command(OnCancel);

            // Dependency Injection kann hier verwendet werden
            _savedMoneyService = new SavedMoneyServiceAPI(new ConfigurationBuilder().Build());
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
        public async Task ExecuteAddCommandAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var savedMoneyCreateDTO = new SavedMoneyCreateDTO
                {
                    Description = Kurzbeschreibung,
                    Price = decimal.Parse(Preis),
                    Date = SelectedDate
                };

                var response = await _savedMoneyService.CreateAsync(savedMoneyCreateDTO);

                if (response != null && response.IsSuccess)
                {
                    // Zeige Erfolgsmeldung an
                    await Application.Current.MainPage.DisplayAlert("Erfolg", "Artikel erfolgreich hinzugefügt", "OK");

                    // Leere die Eingabefelder nach erfolgreicher Übermittlung
                    Kurzbeschreibung = string.Empty;
                    Preis = string.Empty;
                    SelectedDate = DateTime.Now;
                }
                else
                {
                    // Zeige Fehlermeldung an
                    await Application.Current.MainPage.DisplayAlert("Fehler", "Fehler beim Hinzufügen des Artikels", "OK");
                }
            }
            catch (Exception ex)
            {
                // Zeige Fehlermeldung an
                await Application.Current.MainPage.DisplayAlert("Fehler", "Ein Fehler ist aufgetreten: " + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
=======
>>>>>>> Stashed changes
        private void OnAdd()
        {
            // Logik für das Hinzufügen
            Application.Current.MainPage.Navigation.PopAsync();
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
        }

        private void OnCancel()
        {
<<<<<<< Updated upstream
            // Logik für das Abbrechen
=======
<<<<<<< HEAD
            // Navigiere zurück zur vorherigen Seite
=======
            // Logik für das Abbrechen
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
