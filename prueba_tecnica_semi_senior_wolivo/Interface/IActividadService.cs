using prueba_tecnica_semi_senior_wolivo.Dtos;

namespace prueba_tecnica_semi_senior_wolivo.Interface
{
    public interface IActividadService
    {
        Task<GenericResponse<IEnumerable<ActividadDto>>> GetAllAsync();
        Task<GenericResponse<ActividadDto>> GetByIdAsync(Guid id);
        Task<GenericResponse<ActividadDto>> CreateAsync(ActividadDto dto);
        Task<GenericResponse<ActividadDto>> UpdateAsync(Guid id, ActividadDto dto);
        Task<GenericResponse<string>> DeleteAsync(Guid id);

    }
}