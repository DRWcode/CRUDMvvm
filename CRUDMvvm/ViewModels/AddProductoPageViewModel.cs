using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDMvvm.Models;
using CRUDMvvm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMvvm.ViewModels
{
    public partial class AddProductoPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string? nombre;

        [ObservableProperty]

        public decimal precio;

        [ObservableProperty]
        public string? descripcion;

        [ObservableProperty]
        public int inventario;
        
        private readonly ProductoService _productoService;

        public AddProductoPageViewModel()
        {
            _productoService = new ProductoService();
        }

        public AddProductoPageViewModel (Producto producto)
        {
            Id = producto.Id;
            Nombre = producto.Nombre;
            Precio = producto.Precio;
            Descripcion = producto.Descripcion;
            Inventario = producto.Inventario;
            _productoService = new ProductoService();

        }

      
        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                Producto producto = new Producto
                {
                    Id = Id,
                    Nombre = Nombre,
                    Precio = Precio,
                    Descripcion = Descripcion,
                    Inventario = Inventario,
                };

                if (Validar(producto))
                {
                    if (Id == 0)
                    {
                        _productoService.Insert(producto);
                    }
                    else
                    {
                        _productoService.Update(producto);
                    }
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        private bool Validar(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.Nombre))
                {
                    Alerta("ADVERTENCIA", "Escriba el nombre del producto");
                    return false;
                }
                else if (producto.Precio <= 0)
                {
                    Alerta("ADVERTENCIA", "El precio del producto debe ser mayor que cero");
                    return false;
                }
                else if (string.IsNullOrEmpty(producto.Descripcion))
                {
                    Alerta("ADVERTENCIA", "Escriba la descripción del producto");
                    return false;
                }
                else if (producto.Inventario <= 0)
                {
                    Alerta("ADVERTENCIA", "El inventario del producto debe ser mayor que cero");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
                return false;
            }
        }

        private void Alerta(string tipo, string mensaje)
        {
            Device.BeginInvokeOnMainThread(async () => await App.Current.MainPage.DisplayAlert(tipo, mensaje, "Aceptar"));
        }
    }
}