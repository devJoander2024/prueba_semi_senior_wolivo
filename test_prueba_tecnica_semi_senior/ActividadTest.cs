using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_semi_senior_wolivo.Controllers;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Enum;

namespace test_prueba_tecnica_semi_senior
{
    public class ActividadTest
    {
        private readonly Mock<IActividadService> _mockService;
        private readonly ActividadesController _controller;

        public ActividadTest()
        {
            _mockService = new Mock<IActividadService>();
            _controller = new ActividadesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithActividadList()
        {
            List<ActividadDto> actividades = new List<ActividadDto>
            {
                new ActividadDto
                {
                    Titulo = "Actividad 1",
                    Descripcion = "Descripcion 1",
                    Fecha = DateTime.Now,
                    HorasEstimadas = 5,
                    HorasReales = 4,
                    Estado = Estado.Activo,
                    ProyectoId = Guid.NewGuid()
                },
                new ActividadDto
                {
                    Titulo = "Actividad 2",
                    Descripcion = "Descripcion 2",
                    Fecha = DateTime.Now,
                    HorasEstimadas = 3,
                    HorasReales = 2,
                    Estado = Estado.Inactivo,
                    ProyectoId = Guid.NewGuid()
                }
            };

            _mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new GenericResponse<IEnumerable<ActividadDto>>
                {
                    StatusCode = 200,
                    Data = actividades,
                    Message = "OK"
                });

            IActionResult result = await _controller.GetAll();
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<IEnumerable<ActividadDto>> responseData = Assert.IsType<GenericResponse<IEnumerable<ActividadDto>>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(responseData.Data);
        }

        [Fact]
        public async Task GetById_ReturnsActividad_WhenExists()
        {
            Guid id = Guid.NewGuid();
            ActividadDto actividad = new ActividadDto
            {
                Titulo = "Actividad Test",
                Descripcion = "Descripcion test",
                Fecha = DateTime.Now,
                HorasEstimadas = 6,
                HorasReales = 6,
                Estado = Estado.Activo,
                ProyectoId = Guid.NewGuid()
            };

            _mockService.Setup(s => s.GetByIdAsync(id))
                .ReturnsAsync(new GenericResponse<ActividadDto>
                {
                    StatusCode = 200,
                    Data = actividad,
                    Message = "Found"
                });

            IActionResult result = await _controller.GetById(id);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ActividadDto> responseData = Assert.IsType<GenericResponse<ActividadDto>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Actividad Test", responseData.Data.Titulo);
        }

        [Fact]
        public async Task Create_ReturnsCreatedActividad()
        {
            ActividadDto input = new ActividadDto
            {
                Titulo = "Nueva Actividad",
                Descripcion = "Descripcion nueva",
                Fecha = DateTime.Now,
                HorasEstimadas = 8,
                HorasReales = 0,
                Estado = Estado.Activo,
                ProyectoId = Guid.NewGuid()
            };

            _mockService.Setup(s => s.CreateAsync(input))
                .ReturnsAsync(new GenericResponse<ActividadDto>
                {
                    StatusCode = 201,
                    Data = input,
                    Message = "Created"
                });

            IActionResult result = await _controller.Create(input);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ActividadDto> responseData = Assert.IsType<GenericResponse<ActividadDto>>(response.Value);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal("Nueva Actividad", responseData.Data.Titulo);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedActividad()
        {
            Guid id = Guid.NewGuid();
            ActividadDto input = new ActividadDto
            {
                Titulo = "Editada",
                Descripcion = "Descripcion editada",
                Fecha = DateTime.Now,
                HorasEstimadas = 10,
                HorasReales = 9,
                Estado = Estado.Inactivo,
                ProyectoId = Guid.NewGuid()
            };

            _mockService.Setup(s => s.UpdateAsync(id, input))
                .ReturnsAsync(new GenericResponse<ActividadDto>
                {
                    StatusCode = 200,
                    Data = input,
                    Message = "Updated"
                });

            IActionResult result = await _controller.Update(id, input);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ActividadDto> responseData = Assert.IsType<GenericResponse<ActividadDto>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Editada", responseData.Data.Titulo);
        }

        [Fact]
        public async Task Delete_ReturnsOkMessage()
        {
            Guid id = Guid.NewGuid();

            _mockService.Setup(s => s.DeleteAsync(id))
                .ReturnsAsync(new GenericResponse<string>
                {
                    StatusCode = 200,
                    Data = "Eliminada",
                    Message = "Deleted successfully"
                });

            IActionResult result = await _controller.Delete(id);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<string> responseData = Assert.IsType<GenericResponse<string>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Eliminada", responseData.Data);
        }
    }
}
