using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class AppUser:IdentityUser
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La direccion del usuario es requerida")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "La ciudad del usuario es requerida")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "El pais del usuario es requerida")]
        public string Pais { get; set; }
    }
}
