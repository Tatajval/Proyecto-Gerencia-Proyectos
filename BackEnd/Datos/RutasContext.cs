using System;
using BackEnd.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackEnd.Datos
{
    public partial class RutasContext : DbContext
    {
        public RutasContext()
        {
        }

        public RutasContext(DbContextOptions<RutasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conductores> Conductores { get; set; }
        public virtual DbSet<ConductoresPorUnidad> ConductoresPorUnidad { get; set; }
        public virtual DbSet<EstadosDeUnidad> EstadosDeUnidad { get; set; }
        public virtual DbSet<MontosPorRutaPorUnidad> MontosPorRutaPorUnidad { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesPorUsuario> RolesPorUsuario { get; set; }
        public virtual DbSet<Rutas> Rutas { get; set; }
        public virtual DbSet<Unidades> Unidades { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<VwRecaudacion> VwRecaudacion { get; set; }
        public virtual DbSet<VwUnidadesPorEstado> VwUnidadesPorEstado { get; set; }
        public virtual DbSet<VwUsuariosPorEstado> VwUsuariosPorEstado { get; set; }
        public virtual DbSet<VwUsuariosPorRol> VwUsuariosPorRol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-K2CIHUH;Database=rutas;Integrated Security=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conductores>(entity =>
            {
                entity.HasKey(e => e.IdConductor)
                    .HasName("PK__conducto__33A46A9E4450BF4C");

                entity.ToTable("conductores");

                entity.Property(e => e.IdConductor).HasColumnName("id_conductor");

                entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

                entity.Property(e => e.EstaDisponible).HasColumnName("esta_disponible");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasColumnName("nombre_completo")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConductoresPorUnidad>(entity =>
            {
                entity.ToTable("conductores_por_unidad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdConductor).HasColumnName("id_conductor");

                entity.Property(e => e.IdUnidad).HasColumnName("id_unidad");

                entity.HasOne(d => d.IdConductorNavigation)
                    .WithMany(p => p.ConductoresPorUnidad)
                    .HasForeignKey(d => d.IdConductor)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_conductores_por_unidad_conductor");

                entity.HasOne(d => d.IdUnidadNavigation)
                    .WithMany(p => p.ConductoresPorUnidad)
                    .HasForeignKey(d => d.IdUnidad)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_conductores_por_unidad_unidad");
            });

            modelBuilder.Entity<EstadosDeUnidad>(entity =>
            {
                entity.HasKey(e => e.IdEstadoDeUnidad)
                    .HasName("PK__estados___C4F75B7BC8CDE45D");

                entity.ToTable("estados_de_unidad");

                entity.Property(e => e.IdEstadoDeUnidad).HasColumnName("id_estado_de_unidad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MontosPorRutaPorUnidad>(entity =>
            {
                entity.ToTable("montos_por_ruta_por_unidad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.IdRuta).HasColumnName("id_ruta");

                entity.Property(e => e.IdUnidad).HasColumnName("id_unidad");

                entity.Property(e => e.MontoEstimado)
                    .HasColumnName("monto_estimado")
                    .HasColumnType("money");

                entity.Property(e => e.MontoRecaudado)
                    .HasColumnName("monto_recaudado")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.MontosPorRutaPorUnidad)
                    .HasForeignKey(d => d.IdRuta)
                    .HasConstraintName("fk_montos_por_ruta_por_unidad_ruta");

                entity.HasOne(d => d.IdUnidadNavigation)
                    .WithMany(p => p.MontosPorRutaPorUnidad)
                    .HasForeignKey(d => d.IdUnidad)
                    .HasConstraintName("fk_montos_por_ruta_por_unidad_unidad");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__roles__6ABCB5E070F4FE8D");

                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnName("rol")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolesPorUsuario>(entity =>
            {
                entity.ToTable("roles_por_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolesPorUsuario)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_roles_usuarios_roles");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.RolesPorUsuario)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_roles_usuarios_usuarios");
            });

            modelBuilder.Entity<Rutas>(entity =>
            {
                entity.HasKey(e => e.IdRuta)
                    .HasName("PK__rutas__33C9344F49DFE17B");

                entity.ToTable("rutas");

                entity.Property(e => e.IdRuta).HasColumnName("id_ruta");

                entity.Property(e => e.CantidadDeParadas).HasColumnName("cantidad_de_paradas");

                entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

                entity.Property(e => e.PrecioPorPersona)
                    .HasColumnName("precio_por_persona")
                    .HasColumnType("money");

                entity.Property(e => e.Ruta)
                    .HasColumnName("ruta")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unidades>(entity =>
            {
                entity.HasKey(e => e.IdUnidad)
                    .HasName("PK__unidades__95D7C92BFEEE3A3A");

                entity.ToTable("unidades");

                entity.Property(e => e.IdUnidad).HasColumnName("id_unidad");

                entity.Property(e => e.CapacidadDePasajeros).HasColumnName("capacidad_de_pasajeros");

                entity.Property(e => e.IdEstadoDeUnidad).HasColumnName("id_estado_de_unidad");

                entity.Property(e => e.NumeroDePlaca)
                    .IsRequired()
                    .HasColumnName("numero_de_placa")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoDeUnidadNavigation)
                    .WithMany(p => p.Unidades)
                    .HasForeignKey(d => d.IdEstadoDeUnidad)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_unidades_estados_de_unidad");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuarios__4E3E04AD06F4EFA7");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Contrasena)
                    .HasColumnName("contrasena")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasColumnName("usuario")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRecaudacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_recaudacion");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Ruta)
                    .HasColumnName("ruta")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TotalEstimado)
                    .HasColumnName("total_estimado")
                    .HasColumnType("money");

                entity.Property(e => e.TotalRecaudado)
                    .HasColumnName("total_recaudado")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<VwUnidadesPorEstado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_unidades_por_estado");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Unidades).HasColumnName("unidades");
            });

            modelBuilder.Entity<VwUsuariosPorEstado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_usuarios_por_estado");

                entity.Property(e => e.UsuarioActivo).HasColumnName("estado_usuario");

                entity.Property(e => e.Usuarios).HasColumnName("usuarios");
            });

            modelBuilder.Entity<VwUsuariosPorRol>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_usuarios_por_rol");

                entity.Property(e => e.Rol)
                    .HasColumnName("rol")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RolActivo).HasColumnName("estado_rol");

                entity.Property(e => e.Usuarios).HasColumnName("usuarios");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
