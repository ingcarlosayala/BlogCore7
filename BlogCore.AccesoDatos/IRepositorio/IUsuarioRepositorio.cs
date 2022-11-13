﻿using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio
{
    public interface IUsuarioRepositorio:IRepositorio<AppUser>
    {
        void BloquearUsuario(string IdUsuario);
        void DesbloquearUsuario(string IdUsuario);
    }
}
