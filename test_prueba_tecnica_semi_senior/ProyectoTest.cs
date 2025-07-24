using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_semi_senior_wolivo.Controllers;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Enum;

namespace test_prueba_tecnica_semi_senior
{
    public class ProyectoTest
    {
        private readonly Mock<IProyectoService> _mockService;
        private readonly ProyectosController _controller;

        public ProyectoTest()
        {
            _mockService = new Mock<IProyectoService>();
            _controller = new ProyectosController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithProyectoList()
        {
            List<ProyectoDto> proyectos = new List<ProyectoDto>
            {
                new ProyectoDto { Nombre = "Proyecto 1", Descripcion = "Desc 1", Estado = Estado.Activo },
                new ProyectoDto { Nombre = "Proyecto 2", Descripcion = "Desc 2", Estado = Estado.Inactivo }
            };

            _mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new GenericResponse<IEnumerable<ProyectoDto>>
                {
                    StatusCode = 200,
                    Data = proyectos,
                    Message = "OK"
                });

            IActionResult result = await _controller.GetAll();
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<IEnumerable<ProyectoDto>> responseData = Assert.IsType<GenericResponse<IEnumerable<ProyectoDto>>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(responseData.Data);
        }

        [Fact]
        public async Task GetById_ReturnsProyecto_WhenExists()
        {
            Guid id = Guid.NewGuid();

            ProyectoDto proyecto = new ProyectoDto
            {
                Nombre = "Proyecto Test",
                Descripcion = "Descripción test",
                Estado = Estado.Activo
            };

            _mockService.Setup(s => s.GetByIdAsync(id))
                .ReturnsAsync(new GenericResponse<ProyectoDto>
                {
                    StatusCode = 200,
                    Data = proyecto,
                    Message = "Found"
                });

            IActionResult result = await _controller.GetById(id);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ProyectoDto> responseData = Assert.IsType<GenericResponse<ProyectoDto>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Proyecto Test", responseData.Data.Nombre);
        }

        [Fact]
        public async Task Create_ReturnsCreatedProyecto()
        {
            ProyectoDto input = new ProyectoDto
            {
                Nombre = "Nuevo Proyecto",
                Descripcion = "Algo interesante",
                Estado = Estado.Activo
            };

            _mockService.Setup(s => s.CreateAsync(input))
                .ReturnsAsync(new GenericResponse<ProyectoDto>
                {
                    StatusCode = 201,
                    Data = input,
                    Message = "Created"
                });

            IActionResult result = await _controller.Create(input);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ProyectoDto> responseData = Assert.IsType<GenericResponse<ProyectoDto>>(response.Value);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal("Nuevo Proyecto", responseData.Data.Nombre);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedProyecto()
        {
            Guid id = Guid.NewGuid();
            ProyectoDto input = new ProyectoDto
            {
                Nombre = "Editado",
                Descripcion = "Actualizado",
                Estado = Estado.Activo
            };

            _mockService.Setup(s => s.UpdateAsync(id, input))
                .ReturnsAsync(new GenericResponse<ProyectoDto>
                {
                    StatusCode = 200,
                    Data = input,
                    Message = "Updated"
                });

            IActionResult result = await _controller.Update(id, input);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<ProyectoDto> responseData = Assert.IsType<GenericResponse<ProyectoDto>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Editado", responseData.Data.Nombre);
        }

        [Fact]
        public async Task Delete_ReturnsOkMessage()
        {
            Guid id = Guid.NewGuid();

            _mockService.Setup(s => s.DeleteAsync(id))
                .ReturnsAsync(new GenericResponse<string>
                {
                    StatusCode = 200,
                    Data = "Eliminado",
                    Message = "Deleted successfully"
                });

            IActionResult result = await _controller.Delete(id);
            ObjectResult response = Assert.IsType<ObjectResult>(result);
            GenericResponse<string> responseData = Assert.IsType<GenericResponse<string>>(response.Value);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Eliminado", responseData.Data);
        }
    }
}
