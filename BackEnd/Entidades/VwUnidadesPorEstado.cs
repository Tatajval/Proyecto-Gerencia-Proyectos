using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class VwUnidadesPorEstado
    {
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Display(Name = "Unidades")]
        public int Unidades { get; set; }

        public override string ToString()
        {
            return "Reporte=" + this.GetType().Name + ";";
        }
    }
}
