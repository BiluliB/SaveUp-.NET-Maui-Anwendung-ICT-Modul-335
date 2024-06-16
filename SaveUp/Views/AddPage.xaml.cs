using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace SaveUp.Views
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await AnimateButton(addButtonBoxView);
            // Ihre Logik für den AddCommand hier
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await AnimateButton(cancelButtonBoxView);
            // Ihre Logik für den CancelCommand hier
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
