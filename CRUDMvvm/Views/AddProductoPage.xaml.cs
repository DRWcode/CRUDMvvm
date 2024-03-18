using CRUDMvvm.Models;
using CRUDMvvm.ViewModels;

namespace CRUDMvvm.Views 
{
    public partial class AddProductoPage : ContentPage
    {
        private AddProductoPageViewModel _viewModel;

        public AddProductoPage()
        {
            InitializeComponent();
            _viewModel = new AddProductoPageViewModel();
            this.BindingContext = _viewModel;
        }

        public AddProductoPage(Producto producto)
        {
            InitializeComponent();
            _viewModel = new AddProductoPageViewModel(producto);
            this.BindingContext = _viewModel;
        }
    }
}
