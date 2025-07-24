using Microsoft.EntityFrameworkCore;
using prueba_tecnica_semi_senior_wolivo.Models;
using System.Collections.Generic;

namespace prueba_tecnica_semi_senior_wolivo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.UsuarioId);

            modelBuilder.Entity<Proyecto>()
                .HasKey(p => p.ProyectoId);

            modelBuilder.Entity<Actividad>()
                .HasKey(a => a.ActividadId);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Proyectos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Proyecto>()
                .HasMany(p => p.Actividades)
                .WithOne(a => a.Proyecto)
                .HasForeignKey(a => a.ProyectoId);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion<string>();

            modelBuilder.Entity<Usuario>()
            .Property(u => u.Estado)
            .HasConversion(
                v => v ? "true" : "false",
                v => v == "true"
            );

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Actividad>()
                .Property(a => a.Estado)
                .HasConversion<string>();

            foreach (var entity in new[] { typeof(Usuario), typeof(Proyecto), typeof(Actividad) })
            {
                modelBuilder.Entity(entity).Property(nameof(BaseEntity.FechaCreacion))
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity(entity).Property(nameof(BaseEntity.FechaModificacion))
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAddOrUpdate();
            }
        }
    }
}
