using AutoMapper;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Enum;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Models;

namespace prueba_tecnica_semi_senior_wolivo.Services
{
    public class ActividadService : IActividadService
    {
        private readonly IRepository<Actividad> _actividadRepo;
        private readonly IRepository<Proyecto> _proyectoRepo;
        private readonly IMapper _mapper;

        public ActividadService(
            IRepository<Actividad> actividadRepo,
            IRepository<Proyecto> proyectoRepo,
            IMapper mapper)
        {
            _actividadRepo = actividadRepo;
            _proyectoRepo = proyectoRepo;
            _mapper = mapper;
        }

        public async Task<GenericResponse<IEnumerable<ActividadDto>>> GetAllAsync()
        {
            IEnumerable<Actividad> actividades = await _actividadRepo.GetAllAsync();
            List<Actividad> activas = actividades
                .Where(a => a.Estado == Estado.Activo)
                .ToList();

            IEnumerable<ActividadDto> dto = _mapper.Map<IEnumerable<ActividadDto>>(activas);
            return GenericResponse<IEnumerable<ActividadDto>>.Success(dto);
        }

        public async Task<GenericResponse<ActividadDto>> GetByIdAsync(Guid id)
        {
            Actividad actividad = await _actividadRepo.GetByIdAsync(id);
            if (actividad == null || actividad.Estado == Estado.Inactivo)
            {
                return GenericResponse<ActividadDto>.Fail("Actividad no encontrada", 404);
            }

            ActividadDto dto = _mapper.Map<ActividadDto>(actividad);
            return GenericResponse<ActividadDto>.Success(dto, "Actividad recuperada correctamente!");
        }

        public async Task<GenericResponse<ActividadDto>> CreateAsync(ActividadDto dto)
        {
            Proyecto proyecto = await _proyectoRepo.GetByIdAsync(dto.ProyectoId);

            if (proyecto == null || proyecto.Estado != Estado.Activo)
            {
                return GenericResponse<ActividadDto>.Fail("El proyecto no existe o está inactivo.", 404);
            }

            if (dto.Fecha < proyecto.FechaInicio || dto.Fecha > proyecto.FechaFin)
            {
                return GenericResponse<ActividadDto>.Fail("La fecha de la actividad debe estar dentro del rango de fechas del proyecto.", 400);
            }

            Actividad nuevaActividad = _mapper.Map<Actividad>(dto);
            nuevaActividad.ActividadId = Guid.NewGuid();
            nuevaActividad.Estado = Estado.Activo;

            await _actividadRepo.AddAsync(nuevaActividad);
            await _actividadRepo.SaveAsync();

            ActividadDto result = _mapper.Map<ActividadDto>(nuevaActividad);
            return GenericResponse<ActividadDto>.Success(result, "Actividad creada correctamente!");
        }

        public async Task<GenericResponse<ActividadDto>> UpdateAsync(Guid id, ActividadDto dto)
        {
            Actividad actividad = await _actividadRepo.GetByIdAsync(id);
            if (actividad == null || actividad.Estado == Estado.Inactivo)
            {
                return GenericResponse<ActividadDto>.Fail("Actividad no encontrada", 404);
            }

            Proyecto proyecto = await _proyectoRepo.GetByIdAsync(dto.ProyectoId);
            if (proyecto == null || proyecto.Estado != Estado.Activo)
            {
                return GenericResponse<ActividadDto>.Fail("El proyecto no existe o está inactivo.", 404);
            }

            if (dto.Fecha < proyecto.FechaInicio || dto.Fecha > proyecto.FechaFin)
            {
                return GenericResponse<ActividadDto>.Fail("La fecha de la actividad debe estar dentro del rango de fechas del proyecto.", 400);
            }

            actividad.Titulo = dto.Titulo;
            actividad.Descripcion = dto.Descripcion;
            actividad.Fecha = dto.Fecha;
            actividad.HorasEstimadas = dto.HorasEstimadas;
            actividad.HorasReales = dto.HorasReales;
            actividad.ProyectoId = dto.ProyectoId;

            _actividadRepo.Update(actividad);
            await _actividadRepo.SaveAsync();

            ActividadDto result = _mapper.Map<ActividadDto>(actividad);
            return GenericResponse<ActividadDto>.Success(result, "Actividad actualizada correctamente!");
        }

        public async Task<GenericResponse<string>> DeleteAsync(Guid id)
        {
            Actividad actividad = await _actividadRepo.GetByIdAsync(id);
            if (actividad == null || actividad.Estado == Estado.Inactivo)
            {
                return GenericResponse<string>.Fail("Actividad no encontrada", 404);
            }

            actividad.Estado = Estado.Inactivo;
            _actividadRepo.Update(actividad);
            await _actividadRepo.SaveAsync();

            return GenericResponse<string>.Success("Actividad eliminada correctamente!");
        }
    }
}
