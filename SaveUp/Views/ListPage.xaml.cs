using SaveUp.ViewModels;

namespace SaveUp.Views
{
    /// <summary>
    /// ListPage
    /// </summary>
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListPageViewModel.Instance;
        }

        /// <summary>
        /// Load the Artikel when the page appears
        /// </summary>
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
