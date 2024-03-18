using CRUDMvvm.ViewModels;
namespace CRUDMvvm.Views;

public partial class ProductoMainPage : ContentPage
{
	private ProductoMainPageViewModel _viewModel;

	public ProductoMainPage()
	{
		InitializeComponent();
		_viewModel = new ProductoMainPageViewModel();
		this.BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetAll();
    }

}