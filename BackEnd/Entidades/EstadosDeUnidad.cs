using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Entidades
{
    public partial class EstadosDeUnidad
    {
        public EstadosDeUnidad()
        {
            Unidades = new HashSet<Unidades>();
        }

        [Display(Name = "ID")]
        public int IdEstadoDeUnidad { get; set; }
        [Display(Name = "Estado")]
        public char Estado { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Unidades")]
        public virtual ICollection<Unidades> Unidades { get; set; }

        public override string ToString()
        {
            return "Id=" + IdEstadoDeUnidad + ";Estado=" + Estado + ";Descripcion=" + Descripcion + ";";
        }
    }
}
