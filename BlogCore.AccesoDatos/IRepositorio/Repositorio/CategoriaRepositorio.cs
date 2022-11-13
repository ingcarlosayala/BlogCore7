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
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var CategoriaDB = db.Categoria.FirstOrDefault(c => c.Id.Equals(categoria.Id));
            if (CategoriaDB != null)
            {
                CategoriaDB.Nombre = categoria.Nombre;
                CategoriaDB.Orden = categoria.Orden;
            }
        }

        public IEnumerable<SelectListItem> ListaCategoria()
        {
            return db.Categoria.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });
        }
    }
}
