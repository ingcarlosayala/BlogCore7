using BlogCore.AccesoDatos.IRepositorio;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductosController(IUnidadTrabajo unidadTrabajo,IWebHostEnvironment hostEnvironment)
        {
            this.unidadTrabajo = unidadTrabajo;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Models.Producto(),
                ListaCategoria = unidadTrabajo.Categoria.ListaCategoria()
            };

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string WebRootPath = hostEnvironment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                if (productoVM.Producto.Id == 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(WebRootPath, @"imagenes\producto");
                    var Extension = Path.GetExtension(Files[0].FileName);

                    using (var FileStrems = new FileStream(Path.Combine(Upload,FileName + Extension),FileMode.Create))
                    {
                        Files[0].CopyTo(FileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + FileName + Extension;
                    productoVM.Producto.FechaCreacion = DateTime.Now.ToString();

                    unidadTrabajo.Producto.Add(productoVM.Producto);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }

                productoVM.ListaCategoria = unidadTrabajo.Categoria.ListaCategoria();
            }
            return View(productoVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Models.Producto(),
                ListaCategoria = unidadTrabajo.Categoria.ListaCategoria()
            };

            if (id is null)
            {
                return NotFound();
            }
            productoVM.Producto = unidadTrabajo.Producto.Get(id.GetValueOrDefault());

            if (productoVM.Producto is null)
            {
                return NotFound();
            }
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string WebRootPath = hostEnvironment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                var ProductoDb = unidadTrabajo.Producto.Get(productoVM.Producto.Id);

                if (Files.Count() > 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(WebRootPath,@"imagenes\producto");
                    var Extension = Path.GetExtension(Files[0].FileName);
                    var NuevaExtension = Path.GetExtension(Files[0].FileName);

                    var ImagenRuta = Path.Combine(WebRootPath,ProductoDb.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(ImagenRuta))
                    {
                        System.IO.File.Delete(ImagenRuta);
                    }

                    using (var FileStrems = new FileStream(Path.Combine(Upload, FileName + NuevaExtension),FileMode.Create))
                    {
                        Files[0].CopyTo(FileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + FileName + NuevaExtension;
                    productoVM.Producto.FechaCreacion = DateTime.Now.ToString();

                    unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    productoVM.Producto.ImagenUrl = ProductoDb.ImagenUrl;
                }

                productoVM.ListaCategoria = unidadTrabajo.Categoria.ListaCategoria();

                unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todo = unidadTrabajo.Producto.GetAll(incluirPropiedad: "Categoria");
            return Json(new {data = todo});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var ProductoDB = unidadTrabajo.Producto.Get(id.GetValueOrDefault());

            if (id is null)
            {
                return Json(new {success = false, message = "Error al elimina el producto"});
            }

            string WebRootPath = hostEnvironment.WebRootPath;

            var ImagenRuta = Path.Combine(WebRootPath,ProductoDB.ImagenUrl.TrimStart('\\'));

            if (System.IO.File.Exists(ImagenRuta))
            {
                System.IO.File.Delete(ImagenRuta);
            }

            unidadTrabajo.Producto.Remove(ProductoDB);
            unidadTrabajo.Guardar();
            return Json(new {success = true, message = "Producto Eliminado Correctamente"});
        }
        #endregion
    }
}
