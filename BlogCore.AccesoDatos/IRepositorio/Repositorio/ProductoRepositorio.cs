using BlogCore.AccesoDatos.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Producto producto)
        {
            var ProductoDB = db.Producto.FirstOrDefault(p => p.Id.Equals(producto.Id));
            if (ProductoDB != null)
            {
                ProductoDB.Nombre = producto.Nombre;
                ProductoDB.Descripcion = producto.Descripcion;
                ProductoDB.IdCategoria = producto.IdCategoria;
                ProductoDB.ImagenUrl = producto.ImagenUrl;
            }
        }
    }
}
