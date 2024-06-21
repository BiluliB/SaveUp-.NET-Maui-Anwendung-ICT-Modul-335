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
<<<<<<< HEAD
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#EDE9FE"));

            // F�hre den AddCommand aus
            if (BindingContext is AddPageViewModel viewModel)
            {
                await viewModel.ExecuteAddCommandAsync();
=======
            await AnimateButton(addButtonBoxView);
            // Ihre Logik f�r den AddCommand hier
            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.AddCommand.Execute(null);
<<<<<<< Updated upstream
=======
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes
            }
        }

        private async void OnCancelButtonTapped(object sender, EventArgs e)
        {
<<<<<<< HEAD
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#FEE2E2"));

            // F�hre den CancelCommand aus
=======
            await AnimateButton(cancelButtonBoxView);
            // Ihre Logik f�r den CancelCommand hier
<<<<<<< Updated upstream
=======
>>>>>>> 7e7b26512df3c794c9e651cf2f0e699890ec4b86
>>>>>>> Stashed changes
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
