using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Dtos.Report;
using prueba_tecnica_semi_senior_wolivo.Enum;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Models;

namespace prueba_tecnica_semi_senior_wolivo.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IRepository<Usuario> _usuarioRepo;
        private readonly IRepository<Proyecto> _proyectoRepo;
        private readonly IRepository<Actividad> _actividadRepo;

        public ReporteService(
            IRepository<Usuario> usuarioRepo,
            IRepository<Proyecto> proyectoRepo,
            IRepository<Actividad> actividadRepo)
        {
            _usuarioRepo = usuarioRepo;
            _proyectoRepo = proyectoRepo;
            _actividadRepo = actividadRepo;
        }

        public async Task<GenericResponse<ReporteActividadDto>> GetActividadesPorUsuarioAsync(Guid usuarioId, DateTime desde, DateTime hasta)
        {
            Usuario? usuario = await _usuarioRepo.GetByIdAsync(usuarioId);
            if (usuario == null || !usuario.Estado)
            {
                return GenericResponse<ReporteActividadDto>.Fail("Usuario no encontrado o inactivo.", 404);
            }

            IEnumerable<Proyecto> todosLosProyectos = await _proyectoRepo.GetAllAsync();
            List<Proyecto> proyectos = todosLosProyectos
                .Where(p => p.UsuarioId == usuarioId && p.Estado == Estado.Activo)
                .ToList();

            if (proyectos.Count == 0)
            {
                ReporteActividadDto reporteVacio = new ReporteActividadDto
                {
                    Usuario = $"{usuario.Nombres} {usuario.Apellidos}",
                    Proyectos = new List<ProyectoActividadesDto>()
                };

                return GenericResponse<ReporteActividadDto>.Success(reporteVacio, "No se encontraron proyectos activos para el usuario.");
            }

            List<Guid> proyectoIds = proyectos.Select(p => p.ProyectoId).ToList();

            IEnumerable<Actividad> todasLasActividades = await _actividadRepo.GetAllAsync();
            List<Actividad> actividades = todasLasActividades
                .Where(a => a.ProyectoId.HasValue &&
                            proyectoIds.Contains(a.ProyectoId.Value) &&
                            a.Estado == Estado.Activo &&
                            a.Fecha >= desde &&
                            a.Fecha <= hasta)
                .ToList();

            if (actividades.Count == 0)
            {
                ReporteActividadDto reporteSinActividades = new ReporteActividadDto
                {
                    Usuario = $"{usuario.Nombres} {usuario.Apellidos}",
                    Proyectos = new List<ProyectoActividadesDto>()
                };

                return GenericResponse<ReporteActividadDto>.Success(reporteSinActividades, "No se encontraron actividades para el usuario en el rango especificado.");
            }

            List<ProyectoActividadesDto> proyectosAgrupados = new List<ProyectoActividadesDto>();
            foreach (Proyecto proyecto in proyectos)
            {
                List<ActividadReporteDto> actividadesProyecto = actividades
                    .Where(a => a.ProyectoId == proyecto.ProyectoId)
                    .Select(a => new ActividadReporteDto
                    {
                        Fecha = a.Fecha,
                        Titulo = a.Titulo,
                        HorasReales = a.HorasReales
                    })
                    .ToList();

                ProyectoActividadesDto proyectoDto = new ProyectoActividadesDto
                {
                    Nombre = proyecto.Nombre,
                    Actividades = actividadesProyecto
                };

                proyectosAgrupados.Add(proyectoDto);
            }

            ReporteActividadDto reporteFinal = new ReporteActividadDto
            {
                Usuario = $"{usuario.Nombres} {usuario.Apellidos}",
                Proyectos = proyectosAgrupados
            };

            return GenericResponse<ReporteActividadDto>.Success(reporteFinal, "Reporte generado correctamente.");
        }
    }
}
