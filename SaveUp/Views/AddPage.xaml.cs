using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using SaveUp.ViewModels;

namespace SaveUp.Views
{
    public partial class AddPage : ContentPage
    {
        public AddPage(AddPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnAddButtonTapped(object sender, EventArgs e)
        {
            await AnimateButton(addButtonBoxView);
            // Ihre Logik für den AddCommand hier
            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.AddCommand.Execute(null);
            }
        }

        private async void OnCancelButtonTapped(object sender, EventArgs e)
        {
            await AnimateButton(cancelButtonBoxView);
            // Ihre Logik für den CancelCommand hier
            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.CancelCommand.Execute(null);
            }
        }

        private async Task AnimateButton(BoxView boxView)
        {
            boxView.BackgroundColor = Colors.Violet;
            await boxView.FadeTo(1, 50);
            await boxView.FadeTo(0, 500);
            boxView.BackgroundColor = Colors.Transparent;
        }
    }
}
