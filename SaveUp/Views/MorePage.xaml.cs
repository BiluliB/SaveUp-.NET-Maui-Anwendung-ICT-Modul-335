using System.Windows.Input;

namespace SaveUp.Views
{
    /// <summary>
    /// MorePage
    /// </summary>
    public partial class MorePage : ContentPage
    {
        public ICommand OpenPayPalCommand { get; private set; }
        public ICommand OpenDeveloperLinkCommand { get; private set; }

        /// <summary>
        /// Constructor for the MorePage
        /// </summary>
        public MorePage()
        {
            InitializeComponent();
            OpenPayPalCommand = new Command(OpenPayPal);
            OpenDeveloperLinkCommand = new Command(OpenDeveloperLink);
            BindingContext = this;
        }

        /// <summary>
        /// Open the PayPal donation page
        /// </summary>
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

        /// <summary>
        /// Open the developer's GitHub page
        /// </summary>
        private void OpenDeveloperLink()
        {
            try
            {
                Uri uri = new Uri("https://github.com/BiluliB");
                Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }
    }
}
