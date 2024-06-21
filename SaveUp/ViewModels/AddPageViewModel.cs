using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

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

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddPageViewModel()
        {
            AddCommand = new Command(OnAdd);
            CancelCommand = new Command(OnCancel);
        }

        private void OnAdd()
        {
            // Logik für das Hinzufügen
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnCancel()
        {
            // Logik für das Abbrechen
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
