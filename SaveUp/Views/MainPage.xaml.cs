using Microsoft.Maui.Controls;
using SaveUp.ViewModels;

namespace SaveUp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadEinsparungen().ConfigureAwait(false);
        }
    }
}
