using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDMvvm.Models;
using CRUDMvvm.Services;
using CRUDMvvm.Views;
using System;
using System.Collections.ObjectModel;



namespace CRUDMvvm.ViewModels
{
    public partial class ProductoMainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> productoCollection = new ObservableCollection<Producto>();

        private readonly ProductoService _productoService;

        public ProductoMainPageViewModel()
        {
            _productoService = new ProductoService();
        }

        /// <summary>
        /// Obtiene el listado de productos
        /// </summary>
        public void GetAll()
        {
            var getAll = _productoService.GetAll();

            if (getAll?.Count > 0)
            {
                ProductoCollection.Clear();
                foreach (var producto in getAll)
                {
                    ProductoCollection.Add(producto);
                }
            }
        }

        /// <summary>
        /// Redirecciona al formulario de Producto
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GoToAddProductoPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddProductoPage());
        }

        /// <summary>
        /// Selecciona el registro para editar o eliminar
        /// </summary>
        /// <param name="producto">Objeto a editar o eliminar</param>
        /// <returns>Actualizar: Nos lleva al formulario de Producto, Eliminar: Elimina el registro</returns>
        [RelayCommand]
        private async Task SelectProducto(Producto producto)
        {
            try
            {
                string res = await App.Current.MainPage.DisplayActionSheet("Operación", "Cancelar", null, "Actualizar", "Eliminar");

                switch (res)
                {
                    case "Actualizar":
                        await App.Current.MainPage.Navigation.PushAsync(new AddProductoPage(producto));
                        break;
                    case "Eliminar":
                        bool respuesta = await App.Current.MainPage.DisplayAlert("Eliminar Producto", "¿Desea eliminar el producto?", "Si", "No");

                        if (respuesta)
                        {
                            int del = _productoService.Delete(producto);
                            if (del > 0)
                            {
                                ProductoCollection.Remove(producto);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        /// <summary>
        /// Método personalizado para construir alertas
        /// </summary>
        /// <param name="Tipo">Tipo de Alerta</param>
        /// <param name="Mensaje">Mensaje de Alerta</param>
        private void Alerta(string Tipo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current.MainPage.DisplayAlert(Tipo, Mensaje, "Aceptar"));
        }
    }
}