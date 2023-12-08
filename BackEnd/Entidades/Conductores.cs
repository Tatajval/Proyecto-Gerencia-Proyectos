using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class Conductores
    {
        public Conductores()
        {
            ConductoresPorUnidad = new HashSet<ConductoresPorUnidad>();
        }

        [Display(Name = "ID Conductor")]
        public int IdConductor { get; set; }
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }
        [Display(Name = "Activo?")]
        public bool EstaActivo { get; set; }
        [Display(Name = "Disponible?")]
        public bool EstaDisponible { get; set; }

        [Display(Name = "Unidades")]
        public virtual ICollection<ConductoresPorUnidad> ConductoresPorUnidad { get; set; }

        public override string ToString()
        {
            return "IdConductor=" + IdConductor + ";NombreCompleto=" + NombreCompleto + ";EstaActivo=" + EstaActivo + ";EstaDisponible=" + EstaDisponible + ";";
        }
    }
}
