using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using SaveUp.ViewModels;

namespace SaveUp.Views
{
    public partial class AddPage : ContentPage
    {
        private Color originalAddButtonColor;
        private Color originalCancelButtonColor;

        public AddPage(AddPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Speichern der urspr�nglichen Farben beim ersten Anzeigen der Seite
            originalAddButtonColor = AddButton.BackgroundColor;
            originalCancelButtonColor = CancelButton.BackgroundColor;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#EDE9FE"));

            // F�hre den AddCommand aus
            if (BindingContext is AddPageViewModel viewModel)
            {
                await viewModel.ExecuteAddCommandAsync();
            }

            // Setze die urspr�ngliche Farbe zur�ck
            button.BackgroundColor = originalAddButtonColor;
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#FEE2E2"));

            // F�hre den CancelCommand aus
            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.CancelCommand.Execute(null);
            }

            // Setze die urspr�ngliche Farbe zur�ck
            button.BackgroundColor = originalCancelButtonColor;
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
