using BackEnd.Entidades;
using System.Collections.Generic;

namespace BackEnd.Datos
{
    public interface IRegistroActividades
    {
        void Agregar(Actividad registro);

        List<Actividad> ListarTodo();
    }
}

