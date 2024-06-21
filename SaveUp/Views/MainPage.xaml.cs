using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using SaveUp.ViewModels;
using SaveUp.Services;
using Microsoft.Extensions.Configuration;

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
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await _viewModel.LoadEinsparungen();
        }
    }
}
