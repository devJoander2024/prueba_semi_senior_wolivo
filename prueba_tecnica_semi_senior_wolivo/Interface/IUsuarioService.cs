using prueba_tecnica_semi_senior_wolivo.Dtos;

namespace prueba_tecnica_semi_senior_wolivo.Interface
{
    public interface IUsuarioService
    {
        Task<GenericResponse<IEnumerable<UsuarioDto>>> GetAllAsync();
        Task<GenericResponse<UsuarioDto>> CreateAsync(UsuarioDto dto);
        Task<GenericResponse<UsuarioDto>> GetByIdAsync(Guid id);
        Task<GenericResponse<UsuarioDto>> UpdateAsync(Guid id, UsuarioDto dto);
        Task<GenericResponse<string>> DeleteAsync(Guid id);
    }
}
