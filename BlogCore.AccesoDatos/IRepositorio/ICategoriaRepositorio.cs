using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio
{
    public interface ICategoriaRepositorio:IRepositorio<Categoria>
    {
        void Actualizar(Categoria categoria);
        IEnumerable<SelectListItem> ListaCategoria();
    }
}
