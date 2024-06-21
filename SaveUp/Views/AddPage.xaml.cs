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

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#EDE9FE"));

            // Führe den AddCommand aus
            if (BindingContext is AddPageViewModel viewModel)
            {
                await viewModel.ExecuteAddCommandAsync();
            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#FEE2E2"));

            // Führe den CancelCommand aus
            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.CancelCommand.Execute(null);
            }
        }

        private async Task AnimateButtonBackground(Button button, Color highlightColor)
        {
            var originalColor = button.BackgroundColor;
            button.BackgroundColor = highlightColor;
            await Task.Delay(100); // Zeit in Millisekunden
            button.BackgroundColor = originalColor;
        }
    }
}
