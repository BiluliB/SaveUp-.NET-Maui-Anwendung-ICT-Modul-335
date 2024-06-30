using Microsoft.Maui.Controls;
using SaveUp.ViewModels;

namespace SaveUp.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListPageViewModel.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ListPageViewModel viewModel)
            {
                await viewModel.LoadArtikel();
            }
        }
    }
}
