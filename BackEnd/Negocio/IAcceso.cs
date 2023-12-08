using BackEnd.Datos;
using BackEnd.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Negocio
{
    public interface IAcceso
    {
        int UsuarioValido(Usuarios usuario, string contrasena);
        string MensajeResultadoValidacion(int codErr);
        ICollection<RolesPorUsuario> ConfigurarRoles(Usuarios usuario, List<RolesPorUsuario> rolesPorUsuario, List<Roles> roles);
        ClaimsIdentity Identidad(Usuarios usuario);
    }
}
