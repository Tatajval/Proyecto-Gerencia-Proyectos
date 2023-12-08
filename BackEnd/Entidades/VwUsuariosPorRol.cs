using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class VwUsuariosPorRol
    {
        [Display(Name = "Rol")]
        public string Rol { get; set; }
        [Display(Name = "Rol activo?")]
        public bool RolActivo { get; set; }
        [Display(Name = "Usuarios")]
        public int Usuarios { get; set; }
        public override string ToString()
        {
            return "Reporte=" + this.GetType().Name + ";";
        }
    }
}
