using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            RolesPorUsuario = new HashSet<RolesPorUsuario>();
        }

        [Display(Name = "ID Usuario")]
        public int IdUsuario { get; set; }
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        [Display(Name = "Activar Usuario")]
        public bool EstaActivo { get; set; }

        [Display(Name = "Roles")]
        public virtual ICollection<RolesPorUsuario> RolesPorUsuario { get; set; }

        public override string ToString()
        {
            return "IdUsuario=" + IdUsuario + ";Usuario=" + Usuario + ";Contrasena=" + Contrasena + ";EstaActivo=" + EstaActivo + ";";
        }
    }
}
