using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class RolesPorUsuario
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "ID Usuario")]
        public int IdUsuario { get; set; }
        [Display(Name = "ID Rol")]
        public int IdRol { get; set; }

        [Display(Name = "Rol")]
        public virtual Roles IdRolNavigation { get; set; }
        [Display(Name = "Usuario")]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        public override string ToString()
        {
            return "Id=" + Id + ";IdUsuario=" + IdUsuario + ";IdRol=" + IdRol + ";";
        }
    }
}
