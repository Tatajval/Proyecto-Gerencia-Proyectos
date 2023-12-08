using BackEnd.Datos;
using BackEnd.Entidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Negocio
{
    public class Acceso : IAcceso
    {
        private readonly RutasContext rutas;

        public Acceso()
        {
            rutas = new RutasContext();
        }

        public int UsuarioValido(Usuarios usuario, string contrasena)
        {
            if (usuario == null)
                return 1; // el usuario no existe

            if (!usuario.Contrasena.Equals(contrasena))
                return 2; // la contraseña del usuario no es la correcta

            if (!Convert.ToBoolean(usuario.EstaActivo))
                return 3; // el usuario no está activo

            if (usuario.RolesPorUsuario.Count == 0)
                return 4; // el usuario no tiene roles asignados

            foreach (RolesPorUsuario rol in usuario.RolesPorUsuario)
                if (usuario.RolesPorUsuario.Count == 1 && !rol.IdRolNavigation.EstaActivo)
                    return 5; // el usuario solo tiene 1 rol asignado y está inactivo

            return 0; // el usuario es valido
        }

        public string MensajeResultadoValidacion(int codErr)
        {
            string mensaje = string.Empty;
            string tipoMensaje = "Error=";

            if (codErr == 0)
                tipoMensaje = "Estado=";

            switch (codErr)
            {
                case 0:
                    mensaje = "Usuario válido";
                    break;
                case 1: mensaje = "Usuario no existe";
                    break;
                case 2:
                    mensaje = "Contraseña inválida";
                    break;
                case 3:
                    mensaje = "Usuario inactivo";
                    break;
                case 4:
                    mensaje = "Usuario no tiene roles asignados";
                    break;
                default:
                    mensaje = "Usuario tiene asignado un rol inactivo";
                    break;
            }

            codErr += 1000;

            return tipoMensaje + codErr + ":'" + mensaje + "';";
        }

        public ICollection<RolesPorUsuario> ConfigurarRoles(Usuarios usuario, List<RolesPorUsuario> rolesPorUsuario, List<Roles> roles)
        {
            ICollection<RolesPorUsuario> rolesDelUsuario = new List<RolesPorUsuario>();

            if (usuario != null)
                foreach (RolesPorUsuario rol in rolesPorUsuario)
                    if (rol.IdUsuario == usuario.IdUsuario)
                    {
                        rol.IdRolNavigation = roles.Find(x => x.IdRol == rol.IdRol);

                        rolesDelUsuario.Add(rol);
                    }

            return rolesDelUsuario;
        }

        public ClaimsIdentity Identidad(Usuarios usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("Usuario", usuario.Usuario),
                new Claim(ClaimTypes.Name, usuario.Usuario)
            };

            foreach (RolesPorUsuario _rolDelUsuario in usuario.RolesPorUsuario)
                claims.Add(new Claim(ClaimTypes.Role, _rolDelUsuario.IdRolNavigation.Rol));

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
