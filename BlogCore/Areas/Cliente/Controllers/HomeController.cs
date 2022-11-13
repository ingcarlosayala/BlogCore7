using BlogCore.AccesoDatos.IRepositorio;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                ListaSlider = unidadTrabajo.Slider.GetAll(),
                ListaProducto = unidadTrabajo.Producto.GetAll()
            };

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                ListaCategoria = unidadTrabajo.Categoria.ListaCategoria()
            };

            if (id is null)
            {
                return NotFound();
            }
            productoVM.Producto = unidadTrabajo.Producto.Get(id.GetValueOrDefault());
            return View(productoVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}