using BackEnd.Datos;
using BackEnd.Entidades;
using System;
using System.Collections.Generic;

namespace BackEnd.Negocio
{
    public class Actividades : IActividades
    {
        private readonly IRegistroActividades _registroActividades;

        public Actividades()
        {
            _registroActividades = new RegistroActividades();
        }

        public void Agregar(Actividad actividad)
        {
            _registroActividades.Agregar(actividad);
        }

        public List<Actividad> VerListaCompleta()
        {
            return _registroActividades.ListarTodo();
        }
    }
}
