using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoria es requerida")]
        [Display(Name = "Categoria")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Orden es requerida")]
        [Range(1,int.MaxValue)]
        public int Orden { get; set; }
    }
}
