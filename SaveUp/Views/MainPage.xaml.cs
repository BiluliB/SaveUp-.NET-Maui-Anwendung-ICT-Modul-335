using SaveUp.ViewModels;

namespace SaveUp.Views
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        /// <summary>
        /// Load the Einsparungen when the page appears
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadEinsparungen().ConfigureAwait(false);
        }
    }
}
