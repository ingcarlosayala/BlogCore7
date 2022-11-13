using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion del producto es requerida")]
        public string Descripcion { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImagenUrl { get; set; }
        public string FechaCreacion { get; set; }

        [Required(ErrorMessage = "La Categoria es requerida")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
    }
}
