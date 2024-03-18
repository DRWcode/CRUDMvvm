using CRUDMvvm.Views;

namespace CRUDMvvm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductoMainPage());
        }
    }
}
 