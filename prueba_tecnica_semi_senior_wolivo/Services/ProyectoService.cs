using AutoMapper;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Enum;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Models;

namespace prueba_tecnica_semi_senior_wolivo.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IRepository<Proyecto> _repository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        public ProyectoService(IRepository<Proyecto> repository, IMapper mapper, IUsuarioService usuarioService)
        {
            _repository = repository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<GenericResponse<IEnumerable<ProyectoDto>>> GetAllAsync()
        {
            IEnumerable<Proyecto> proyectos = await _repository.GetAllAsync();
            List<Proyecto> activos = proyectos
                .Where(p => p.Estado == Estado.Activo)
                .ToList();

            IEnumerable<ProyectoDto> dto = _mapper.Map<IEnumerable<ProyectoDto>>(activos);
            return GenericResponse<IEnumerable<ProyectoDto>>.Success(dto, "Proyectos recuperados correctamente!");
        }

        public async Task<GenericResponse<ProyectoDto>> GetByIdAsync(Guid id)
        {
            Proyecto proyecto = await _repository.GetByIdAsync(id);
            if (proyecto == null || proyecto.Estado == Estado.Inactivo)
            {
                return GenericResponse<ProyectoDto>.Fail("El Proyecto no existe o se encuentra Inactivo", 404);
            }

            ProyectoDto dto = _mapper.Map<ProyectoDto>(proyecto);
            return GenericResponse<ProyectoDto>.Success(dto, "Proyecto encontrado correctamente!");
        }

        public async Task<GenericResponse<ProyectoDto>> CreateAsync(ProyectoDto dto)
        {
            GenericResponse<UsuarioDto> usuarioResponse = await _usuarioService.GetByIdAsync(dto.UsuarioId);

            if (usuarioResponse.StatusCode != 200 || usuarioResponse.Data == null)
            {
                GenericResponse<ProyectoDto> responseError = GenericResponse<ProyectoDto>.Fail("El usuario especificado no existe o está inactivo.", 404);
                return responseError;
            }

            Proyecto entity = _mapper.Map<Proyecto>(dto);
            entity.ProyectoId = Guid.NewGuid();
            entity.Estado = Estado.Activo;

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            ProyectoDto result = _mapper.Map<ProyectoDto>(entity);
            return GenericResponse<ProyectoDto>.Success(result, "Proyecto creado correctamente!");
        }

        public async Task<GenericResponse<ProyectoDto>> UpdateAsync(Guid id, ProyectoDto dto)
        {
            Proyecto proyecto = await _repository.GetByIdAsync(id);
            if (proyecto == null || proyecto.Estado == Estado.Inactivo)
            {
                return GenericResponse<ProyectoDto>.Fail("El proyecto no existe o se encuentra Inactivo", 404);
            }

            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;
            proyecto.FechaInicio = dto.FechaInicio;
            proyecto.FechaFin = dto.FechaFin;
            proyecto.Estado = dto.Estado;
            proyecto.UsuarioId = dto.UsuarioId;

            _repository.Update(proyecto);
            await _repository.SaveAsync();

            ProyectoDto result = _mapper.Map<ProyectoDto>(proyecto);
            return GenericResponse<ProyectoDto>.Success(result, "Proyecto actualizado correctamente!");
        }

        public async Task<GenericResponse<string>> DeleteAsync(Guid id)
        {
            Proyecto proyecto = await _repository.GetByIdAsync(id);
            if (proyecto == null || proyecto.Estado == Estado.Inactivo)
            {
                return GenericResponse<string>.Fail("Proyecto no encontrado", 404);
            }

            proyecto.Estado = Estado.Inactivo;

            _repository.Update(proyecto);
            await _repository.SaveAsync();

            return GenericResponse<string>.Success("Proyecto eliminado correctamente!");
        }
    }
}
