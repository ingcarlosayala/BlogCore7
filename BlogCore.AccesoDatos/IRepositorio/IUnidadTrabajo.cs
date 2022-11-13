using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio
{
    public interface IUnidadTrabajo:IDisposable
    {
        ICategoriaRepositorio Categoria { get; }
        ISliderRepositorio Slider { get; }
        IProductoRepositorio Producto { get; }
        IUsuarioRepositorio Usuario { get; }
        void Guardar();
    }
}
