using BlogCore.AccesoDatos.IRepositorio;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public CategoriasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Categoria.Add(categoria);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var CategoriaDB = unidadTrabajo.Categoria.Get(id.GetValueOrDefault());
            if (id is null)
            {
                return NotFound();
            }
            if (CategoriaDB is null)
            {
                return NotFound();
            }

            return View(CategoriaDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Categoria.Actualizar(categoria);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Json(new {data = unidadTrabajo.Categoria.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CategoriaDB = unidadTrabajo.Categoria.Get(id.GetValueOrDefault());
            if (CategoriaDB is null)
            {
                return Json(new {success = false, message = "Error al eliminar la categoria"});
            }
            unidadTrabajo.Categoria.Remove(CategoriaDB);
            unidadTrabajo.Guardar();
            return Json(new {success = true, message = "Categoria Eliminada Correctamente"});
        }
        #endregion
    }
}
