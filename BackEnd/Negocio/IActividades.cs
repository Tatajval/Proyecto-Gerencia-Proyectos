using BackEnd.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Negocio
{
    public interface IActividades
    {
        void Agregar(Actividad actividad);
        List<Actividad> VerListaCompleta();
    }
}
