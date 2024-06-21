

using SaveUp.ViewModels;

namespace SaveUp.Views
{

    public partial class MainPage : ContentPage
    {


        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
    
}
