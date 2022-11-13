using BlogCore.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext db;

        public ICategoriaRepositorio Categoria { get; private set; }
        public ISliderRepositorio Slider { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public IUsuarioRepositorio Usuario { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            this.db = db;
            Categoria= new CategoriaRepositorio(db);
            Slider= new SliderRepositorio(db);
            Producto= new ProductoRepositorio(db);
            Usuario= new UsuarioRepositorio(db);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Guardar()
        {
            db.SaveChanges();
        }
    }
}
