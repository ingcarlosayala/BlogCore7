using BlogCore.AccesoDatos.IRepositorio;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment hostEnvironment;

        public SlidersController(IUnidadTrabajo unidadTrabajo,IWebHostEnvironment hostEnvironment)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string WebRootPath = hostEnvironment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(WebRootPath,@"imagenes\slider");
                    var Extension = Path.GetExtension(Files[0].FileName);

                    using (var FileStrems = new FileStream(Path.Combine(Upload, FileName + Extension),FileMode.Create))
                    {
                        Files[0].CopyTo(FileStrems);
                    }

                    slider.ImagenUrl = @"\imagenes\slider\"+FileName + Extension;

                    unidadTrabajo.Slider.Add(slider);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var SliderDB = unidadTrabajo.Slider.Get(id.GetValueOrDefault());
            if (id is null)
            {
                return NotFound();
            }
            if (SliderDB is  null)
            {
                return NotFound();
            }

            return View(SliderDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string WebRootPath = hostEnvironment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                var SliderDB = unidadTrabajo.Slider.Get(slider.Id);

                if (Files.Count() > 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(WebRootPath, @"imagenes\slider");
                    var Extension = Path.GetExtension(Files[0].FileName);
                    var NuevaExtension = Path.GetExtension(Files[0].FileName);

                    var ImagenRuta = Path.Combine(WebRootPath,SliderDB.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(ImagenRuta))
                    {
                        System.IO.File.Delete(ImagenRuta);
                    }

                    using (var FileStrems = new FileStream(Path.Combine(Upload,FileName + NuevaExtension),FileMode.Create))
                    {
                        Files[0].CopyTo(FileStrems);
                    }

                    slider.ImagenUrl = @"\imagenes\slider\" + FileName + NuevaExtension;

                    unidadTrabajo.Slider.Actualizar(slider);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.ImagenUrl = SliderDB.ImagenUrl;
                }
                unidadTrabajo.Slider.Actualizar(slider);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Json(new { data = unidadTrabajo.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var SliderDB = unidadTrabajo.Slider.Get(id.GetValueOrDefault());
            if (SliderDB is null)
            {
                return Json(new { success = false, message = "Error al eliminar la slider" });
            }
            unidadTrabajo.Slider.Remove(SliderDB);
            unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Slider Eliminada Correctamente" });
        }
        #endregion
    }
}
