using SaveUp.ViewModels;

namespace SaveUp.Views
{
    /// <summary>
    /// AddPage for adding a new entry
    /// </summary>
    public partial class AddPage : ContentPage
    {
        private Color originalAddButtonColor;
        private Color originalCancelButtonColor;

        public AddPage(AddPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        /// <summary>
        /// Set the original colors when the page appears
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            originalAddButtonColor = AddButton.BackgroundColor;
            originalCancelButtonColor = CancelButton.BackgroundColor;
        }

        /// <summary>
        /// Event handler for the AddButtonClicked event
        /// </summary>
        /// <param name="sender"></param>  
        /// <param name="e"></param> 
        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#EDE9FE"));

            if (BindingContext is AddPageViewModel viewModel)
            {
                await viewModel.ExecuteAddCommandAsync();
            }
            button.BackgroundColor = originalAddButtonColor;
        }

        /// <summary>
        /// Event handler for the CancelButtonClicked event
        /// </summary>
        /// <param name="sender"></param>  
        /// <param name="e"></param> 
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await AnimateButtonBackground(button, Color.FromArgb("#FEE2E2"));

            if (BindingContext is AddPageViewModel viewModel)
            {
                viewModel.CancelCommand.Execute(null);
            }

            button.BackgroundColor = originalCancelButtonColor;
        }

        /// <summary>
        /// Animate the background of a button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="highlightColor"></param>
        /// <returns></returns>
        private async Task AnimateButtonBackground(Button button, Color highlightColor)
        {
            var originalColor = button.BackgroundColor;
            button.BackgroundColor = highlightColor;
            await Task.Delay(100);
            button.BackgroundColor = originalColor;
        }
    }
}
