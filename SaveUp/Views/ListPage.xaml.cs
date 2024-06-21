using SaveUp.ViewModels;

namespace SaveUp.Views
{ 

public partial class ListPage : ContentPage
{
	public ListPage(ListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
}