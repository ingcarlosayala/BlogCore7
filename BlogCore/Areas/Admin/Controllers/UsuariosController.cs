using BlogCore.AccesoDatos.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public UsuariosController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(unidadTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            unidadTrabajo.Usuario.BloquearUsuario(id);
            unidadTrabajo.Guardar();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            unidadTrabajo.Usuario.DesbloquearUsuario(id);
            unidadTrabajo.Guardar();
            return RedirectToAction(nameof(Index));
        }
    }
}
