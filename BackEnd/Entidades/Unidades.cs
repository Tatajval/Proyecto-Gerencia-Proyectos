using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class Unidades
    {
        public Unidades()
        {
            ConductoresPorUnidad = new HashSet<ConductoresPorUnidad>();
            MontosPorRutaPorUnidad = new HashSet<MontosPorRutaPorUnidad>();
        }

        [Display(Name = "ID Unidad")]
        public int IdUnidad { get; set; }
        [Display(Name = "Número de placa")]
        public string NumeroDePlaca { get; set; }
        [Display(Name = "Capacidad de pasajeros")]
        public int CapacidadDePasajeros { get; set; }
        [Display(Name = "ID Estado de la unidad")]
        public int IdEstadoDeUnidad { get; set; }

        [Display(Name = "Estado")]
        public virtual EstadosDeUnidad IdEstadoDeUnidadNavigation { get; set; }
        [Display(Name = "Conductor")]
        public virtual ICollection<ConductoresPorUnidad> ConductoresPorUnidad { get; set; }
        [Display(Name = "Montos")]
        public virtual ICollection<MontosPorRutaPorUnidad> MontosPorRutaPorUnidad { get; set; }

        public override string ToString()
        {
            return "IdUnidad=" + IdUnidad + ";NumeroDePlaca=" + NumeroDePlaca + ";CapacidadDePasajeros=" + CapacidadDePasajeros + ";IdEstadoDeUnidad=" + IdEstadoDeUnidad + ";";
        }
    }
}
