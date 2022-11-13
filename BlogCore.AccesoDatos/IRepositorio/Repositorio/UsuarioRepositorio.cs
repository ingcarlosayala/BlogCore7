using BlogCore.AccesoDatos.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio.Repositorio
{
    internal class UsuarioRepositorio : Repositorio<AppUser>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext db;

        public UsuarioRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var UsuarioDB = db.AppUser.FirstOrDefault(u => u.Id.Equals(IdUsuario));
            UsuarioDB.LockoutEnd = DateTime.Now.AddDays(1);
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var UsuarioDB = db.AppUser.FirstOrDefault(u => u.Id.Equals(IdUsuario));
            UsuarioDB.LockoutEnd = DateTime.Now;
        }
    }
}
