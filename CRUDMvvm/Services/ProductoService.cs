using CRUDMvvm.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMvvm.Services
{
    public class ProductoService
    {
        private readonly SQLiteConnection _dbConnection;

        public ProductoService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Producto.db3");
            _dbConnection = new SQLiteConnection(dbPath);
            _dbConnection.CreateTable<Producto>();
        }

        public List<Producto> GetAll() 
        {
            var res = _dbConnection.Table<Producto>().ToList();
            return res;
        }

        public int Insert(Producto producto)
        {
            return _dbConnection.Insert(producto);
        }

        public int Update(Producto producto)
        {
            return _dbConnection.Update(producto);
        }

        public int Delete(Producto producto)
        {
            return _dbConnection.Delete(producto);
        }
    }
}
