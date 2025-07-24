using prueba_tecnica_semi_senior_wolivo.Dtos.Report;
using prueba_tecnica_semi_senior_wolivo.Dtos;

namespace prueba_tecnica_semi_senior_wolivo.Interface
{
    public interface IReporteService
    {
        Task<GenericResponse<ReporteActividadDto>> GetActividadesPorUsuarioAsync(Guid usuarioId, DateTime desde, DateTime hasta);
    }
}
