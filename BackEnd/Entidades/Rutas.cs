using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class Rutas
    {
        public Rutas()
        {
            MontosPorRutaPorUnidad = new HashSet<MontosPorRutaPorUnidad>();
        }

        [Display(Name = "ID Ruta")]
        public int IdRuta { get; set; }
        [Display(Name = "Ruta")]
        public string Ruta { get; set; }
        [Display(Name = "Cantidad de paradas")]
        public int CantidadDeParadas { get; set; }
        [Display(Name = "Precio")]
        public decimal PrecioPorPersona { get; set; }
        [Display(Name = "Activa?")]
        public bool EstaActivo { get; set; }

        [Display(Name = "Montos")]
        public virtual ICollection<MontosPorRutaPorUnidad> MontosPorRutaPorUnidad { get; set; }

        public override string ToString()
        {
            return "IdRuta=" + IdRuta + ";Ruta=" + Ruta + ";CantidadDeParadas=" + CantidadDeParadas + ";PrecioPorPersona=" + PrecioPorPersona + ";EstaActivo=" + EstaActivo + ";";
        }
    }
}
