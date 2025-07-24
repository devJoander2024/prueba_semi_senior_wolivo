using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;

namespace prueba_tecnica_semi_senior_wolivo.Controllers
{
    [ApiController]
    [Route("api/proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoService _service;

        public ProyectosController(IProyectoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GenericResponse<IEnumerable<ProyectoDto>> response = await _service.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GenericResponse<ProyectoDto> response = await _service.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProyectoDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<string> errores = new List<string>();

                foreach (ModelStateEntry entry in ModelState.Values)
                {
                    foreach (ModelError error in entry.Errors)
                    {
                        errores.Add(error.ErrorMessage);
                    }
                }

                GenericResponse<ProyectoDto> responseError = GenericResponse<ProyectoDto>.Fail(errores, 400);
                return BadRequest(responseError);
            }
            GenericResponse<ProyectoDto> response = await _service.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProyectoDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<string> errores = new List<string>();

                foreach (ModelStateEntry entry in ModelState.Values)
                {
                    foreach (ModelError error in entry.Errors)
                    {
                        errores.Add(error.ErrorMessage);
                    }
                }

                GenericResponse<ProyectoDto> responseError = GenericResponse<ProyectoDto>.Fail(errores, 400);
                return BadRequest(responseError);
            }
            GenericResponse<ProyectoDto> response = await _service.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            GenericResponse<string> response = await _service.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
