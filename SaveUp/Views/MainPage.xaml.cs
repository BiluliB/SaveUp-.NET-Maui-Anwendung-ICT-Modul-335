using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using SaveUp.ViewModels;
using SaveUp.Services;
using Microsoft.Extensions.Configuration;

using SaveUp.ViewModels;

using SaveUp.ViewModels;

namespace SaveUp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

<<<<<<< Updated upstream

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
=======
<<<<<<< HEAD
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
=======

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes
        }
    }
}
