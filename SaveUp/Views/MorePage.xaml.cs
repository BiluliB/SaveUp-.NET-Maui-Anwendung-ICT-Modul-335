using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace SaveUp.Views
{
    public partial class MorePage : ContentPage
    {
        public ICommand OpenPayPalCommand { get; private set; }
        public ICommand OpenDeveloperLinkCommand { get; private set; }

        public MorePage()
        {
            InitializeComponent();
            OpenPayPalCommand = new Command(OpenPayPal);
            OpenDeveloperLinkCommand = new Command(OpenDeveloperLink);
            BindingContext = this;
        }

        private void OpenPayPal()
        {
            try
            {
                Uri uri = new Uri("https://www.paypal.com/donate/?hosted_button_id=9HUYH98XJAS64");
                Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        private void OpenDeveloperLink()
        {
            try
            {
                Uri uri = new Uri("https://github.com/BiluliB"); // Ersetzen Sie dies durch den tatsächlichen Link zum Entwicklerprofil oder zur Website
                Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }
    }
}
