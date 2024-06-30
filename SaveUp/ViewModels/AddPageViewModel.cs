using Microsoft.Extensions.Configuration;
using SaveUp.Interfaces;
using SaveUp.Services;
using SaveUpModels.DTOs.Requests;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SaveUp.ViewModels
{
    /// <summary>
    /// ViewModel for the AddPage
    /// </summary>
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

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly ISavedMoneyServiceAPI _savedMoneyService;

        /// <summary>
        /// Constructor for the AddPageViewModel
        /// </summary>
        public AddPageViewModel()
        {
            AddCommand = new Command(async () => await ExecuteAddCommandAsync(), () => IsNotBusy);
            CancelCommand = new Command(OnCancel);

            _savedMoneyService = new SavedMoneyServiceAPI(new ConfigurationBuilder().Build());
        }
        /// <summary>
        /// Execute the AddCommand
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAddCommandAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(Kurzbeschreibung))
                {
                    await Application.Current.MainPage.DisplayAlert("Fehler", "Bitte eine Kurzbeschreibung eingeben", "OK");
                    IsBusy = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(Preis) || !decimal.TryParse(Preis, out decimal preisValue))
                {
                    await Application.Current.MainPage.DisplayAlert("Fehler", "Bitte einen gültigen Preis eingeben", "OK");
                    IsBusy = false;
                    return;
                }

                var savedMoneyCreateDTO = new SavedMoneyCreateDTO
                {
                    Description = Kurzbeschreibung,
                    Price = preisValue,
                    Date = SelectedDate 
                };

                var response = await _savedMoneyService.CreateAsync(savedMoneyCreateDTO);

                if (response != null && response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Erfolg", "Artikel erfolgreich hinzugefügt", "OK");

                    Kurzbeschreibung = string.Empty;
                    Preis = string.Empty;
                    SelectedDate = DateTime.Now;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fehler", "Fehler beim Hinzufügen des Artikels", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fehler", "Ein Fehler ist aufgetreten: " + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Execute the CancelCommand
        /// </summary>
        private void OnCancel()
        {
            Kurzbeschreibung = string.Empty;
            SelectedDate = DateTime.Now;
            Preis = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
