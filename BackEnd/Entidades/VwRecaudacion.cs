using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class VwRecaudacion
    {
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
        [Display(Name = "Ruta")]
        public string Ruta { get; set; }
        [Display(Name = "Total estimado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalEstimado { get; set; }
        [Display(Name = "Total recaudado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalRecaudado { get; set; }

        public override string ToString()
        {
            return "Reporte=" + this.GetType().Name + ";";
        }
    }
}
