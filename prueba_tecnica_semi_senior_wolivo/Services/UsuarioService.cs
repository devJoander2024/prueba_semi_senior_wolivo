using AutoMapper;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Models;

namespace prueba_tecnica_semi_senior_wolivo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GenericResponse<IEnumerable<UsuarioDto>>> GetAllAsync()
        {
            IEnumerable<Usuario> usuarios = await _repository.GetAllAsync();
            List<Usuario> activos = usuarios.Where(u => u.Estado).ToList();

            IEnumerable<UsuarioDto> dto = _mapper.Map<IEnumerable<UsuarioDto>>(activos);
            return GenericResponse<IEnumerable<UsuarioDto>>.Success(dto, "Usuarios recuperados correctamente!");
        }

        public async Task<GenericResponse<UsuarioDto>> GetByIdAsync(Guid id)
        {
            Usuario usuario = await _repository.GetByIdAsync(id);
            if (usuario == null || !usuario.Estado)
            {
                return GenericResponse<UsuarioDto>.Fail("El usuario no existe o está inactivo", 404);
            }

            UsuarioDto dto = _mapper.Map<UsuarioDto>(usuario);
            return GenericResponse<UsuarioDto>.Success(dto, "Usuario encontrado exitosamente!");
        }
        public async Task<GenericResponse<UsuarioDto>> CreateAsync(UsuarioDto dto)
        {
            Usuario entity = _mapper.Map<Usuario>(dto);
            entity.UsuarioId = Guid.NewGuid();
            entity.Estado = true;

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            UsuarioDto result = _mapper.Map<UsuarioDto>(entity);
            return GenericResponse<UsuarioDto>.Success(result, "Usuario creado exitrosamente!");
        }

        public async Task<GenericResponse<UsuarioDto>> UpdateAsync(Guid id, UsuarioDto dto)
        {
            Usuario usuario = await _repository.GetByIdAsync(id);
            if (usuario == null || !usuario.Estado)
            {
                return GenericResponse<UsuarioDto>.Fail("El usuario no existe o está inactivo", 404);
            }

            usuario.Nombres = dto.Nombres;
            usuario.Apellidos = dto.Apellidos;
            usuario.Correo = dto.Correo;
            usuario.Telefono = dto.Telefono;
            usuario.Rol = dto.Rol;

            _repository.Update(usuario);
            await _repository.SaveAsync();

            UsuarioDto result = _mapper.Map<UsuarioDto>(usuario);
            return GenericResponse<UsuarioDto>.Success(result, "Usuario actualizado exitosamente!");
        }

        public async Task<GenericResponse<string>> DeleteAsync(Guid id)
        {
            Usuario usuario = await _repository.GetByIdAsync(id);
            if (usuario == null || !usuario.Estado)
            {
                return GenericResponse<string>.Fail("Usuario no encontrado", 404);
            }

            usuario.Estado = false;
            _repository.Update(usuario);
            await _repository.SaveAsync();

            return GenericResponse<string>.Success(null, "Usuario eliminado correctamente!");
        }
    }
}
