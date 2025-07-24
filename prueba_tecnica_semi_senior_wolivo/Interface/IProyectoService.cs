using prueba_tecnica_semi_senior_wolivo.Dtos;

namespace prueba_tecnica_semi_senior_wolivo.Interface
{
    public interface IProyectoService
    {
        Task<GenericResponse<IEnumerable<ProyectoDto>>> GetAllAsync();
        Task<GenericResponse<ProyectoDto>> GetByIdAsync(Guid id);
        Task<GenericResponse<ProyectoDto>> CreateAsync(ProyectoDto dto);
        Task<GenericResponse<ProyectoDto>> UpdateAsync(Guid id, ProyectoDto dto);
        Task<GenericResponse<string>> DeleteAsync(Guid id);
    }
}
