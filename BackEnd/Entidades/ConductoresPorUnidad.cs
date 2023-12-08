using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class ConductoresPorUnidad
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "ID Unidad")]
        public int IdUnidad { get; set; }
        [Display(Name = "ID Conductor")]
        public int IdConductor { get; set; }

        [Display(Name = "Conductor")]
        public virtual Conductores IdConductorNavigation { get; set; }
        [Display(Name = "Unidad")]
        public virtual Unidades IdUnidadNavigation { get; set; }

        public override string ToString()
        {
            return "Id=" + Id + ";IdUnidad=" + IdUnidad + ";IdConductor=" + IdConductor + ";";
        }
    }
}
