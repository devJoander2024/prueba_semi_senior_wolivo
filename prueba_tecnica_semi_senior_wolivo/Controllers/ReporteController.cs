using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using prueba_tecnica_semi_senior_wolivo.Dtos.Report;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;

namespace prueba_tecnica_semi_senior_wolivo.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("actividades")]
        public async Task<IActionResult> GetActividadesPorUsuario(
            [FromQuery] Guid usuarioId,
            [FromQuery] string fechaDesde,
            [FromQuery] string fechaHasta)
        {
            if (usuarioId == Guid.Empty)
            {
                return BadRequest(new { error = "El parámetro 'usuarioId' es obligatorio y no puede estar vacío." });
            }

            DateTime fechasDesde;
            DateTime fechasHasta;

            bool fechaDesdeValida = DateTime.TryParseExact(
                fechaDesde,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechasDesde
            );

            bool fechaHastaValida = DateTime.TryParseExact(
                fechaDesde,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechasHasta
            );

            if (!fechaDesdeValida || !fechaHastaValida)
            {
                return BadRequest(new { error = "Las fechas 'desde' y 'hasta' deben tener el formato 'yyyy-MM-dd'." });
            }

            if (fechasDesde > fechasHasta)
            {
                return BadRequest(new { error = "La fecha 'desde' no puede ser mayor que la fecha 'hasta'." });
            }

            GenericResponse<ReporteActividadDto> response = await _reporteService.GetActividadesPorUsuarioAsync(usuarioId, fechasDesde, fechasHasta);
            return StatusCode(response.StatusCode, response);
        }
    }
}
