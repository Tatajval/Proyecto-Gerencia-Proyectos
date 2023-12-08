using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class Roles
    {
        public Roles()
        {
            RolesPorUsuario = new HashSet<RolesPorUsuario>();
        }

        [Display(Name = "ID Rol")]
        public int IdRol { get; set; }
        [Display(Name = "Rol")]
        public string Rol { get; set; }
        [Display(Name = "Activo")]
        public bool EstaActivo { get; set; }

        [Display(Name = "Usuarios")]
        public virtual ICollection<RolesPorUsuario> RolesPorUsuario { get; set; }

        public override string ToString()
        {
            return "IdRol=" + IdRol + ";Rol=" + Rol + ";EstaActivo=" + EstaActivo + ";";
        }
    }
}
