using SaveUp.ViewModels;

namespace SaveUp.Views;

public partial class MorePage : ContentPage
{
	public MorePage(MorePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}