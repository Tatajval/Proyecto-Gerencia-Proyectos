using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class MontosPorRutaPorUnidad
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
        [Display(Name = "ID Ruta")]
        public int IdRuta { get; set; }
        [Display(Name = "ID Unidad")]
        public int IdUnidad { get; set; }
        [Display(Name = "Monto estimado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal MontoEstimado { get; set; }
        [Display(Name = "Monto recaudado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal MontoRecaudado { get; set; }

        [Display(Name = "Ruta")]
        public virtual Rutas IdRutaNavigation { get; set; }
        [Display(Name = "Unidad")]
        public virtual Unidades IdUnidadNavigation { get; set; }

        public override string ToString()
        {
            return "Id=" + Id + ";Fecha=" + Fecha + ";IdRuta=" + IdRuta + ";IdUnidad=" + IdUnidad + ";MontoEstimado=" + MontoEstimado + ";MontoRecaudado=" + MontoRecaudado + ";";
        }
    }
}
