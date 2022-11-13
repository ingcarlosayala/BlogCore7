using BlogCore.AccesoDatos.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio.Repositorio
{
    public class SliderRepositorio : Repositorio<Slider>, ISliderRepositorio
    {
        private readonly ApplicationDbContext db;

        public SliderRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Slider slider)
        {
            var SliderDB = db.Slider.FirstOrDefault(s => s.Id.Equals(slider.Id));
            if (SliderDB != null)
            {
                SliderDB.Nombre = slider.Nombre;
                SliderDB.Estado = slider.Estado;
                SliderDB.ImagenUrl = slider.ImagenUrl;
            }
        }
    }
}
