using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_semi_senior_wolivo.Controllers;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Enum;

namespace test_prueba_tecnica_semi_senior
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> _mockService;
        private readonly UsuariosController _controller;

        public UsuarioControllerTest()
        {
            _mockService = new Mock<IUsuarioService>();
            _controller = new UsuariosController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithUsuariosList()
        {
            List<UsuarioDto> usuarios = new List<UsuarioDto>
            {
                new UsuarioDto { Nombres = "Joan", Apellidos = "Doe", Correo = "joan@example.com", Telefono = "123", Estado = true, Rol = Rol.Admin },
                new UsuarioDto { Nombres = "Ana", Apellidos = "Smith", Correo = "ana@example.com", Telefono = "456", Estado = true, Rol = Rol.Editor }
            };

            _mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new GenericResponse<IEnumerable<UsuarioDto>>
                {
                    StatusCode = 200,
                    Data = usuarios,
                    Message = "Success"
                });

            IActionResult result = await _controller.GetAll();
            ObjectResult okResult = Assert.IsType<ObjectResult>(result);
            GenericResponse<IEnumerable<UsuarioDto>> response = Assert.IsType<GenericResponse<IEnumerable<UsuarioDto>>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenUsuarioExists()
        {
            Guid userId = Guid.NewGuid();

            UsuarioDto usuario = new UsuarioDto
            {
                Nombres = "Carlos",
                Apellidos = "López",
                Correo = "carlos@example.com",
                Telefono = "789",
                Estado = true,
                Rol = Rol.Viewer
            };

            _mockService.Setup(s => s.GetByIdAsync(userId))
                .ReturnsAsync(new GenericResponse<UsuarioDto>
                {
                    StatusCode = 200,
                    Data = usuario,
                    Message = "Found"
                });

            IActionResult result = await _controller.GetById(userId);
            ObjectResult okResult = Assert.IsType<ObjectResult>(result);
            GenericResponse<UsuarioDto> response = Assert.IsType<GenericResponse<UsuarioDto>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Carlos", response.Data.Nombres);
        }

        [Fact]
        public async Task Create_ReturnsCreatedUsuario()
        {
            UsuarioDto input = new UsuarioDto
            {
                Nombres = "Laura",
                Apellidos = "Martínez",
                Correo = "laura@example.com",
                Telefono = "321",
                Estado = true,
                Rol = Rol.Editor
            };

            _mockService.Setup(s => s.CreateAsync(input))
                .ReturnsAsync(new GenericResponse<UsuarioDto>
                {
                    StatusCode = 201,
                    Data = input,
                    Message = "Created"
                });

            IActionResult result = await _controller.Create(input);
            ObjectResult createdResult = Assert.IsType<ObjectResult>(result);
            GenericResponse<UsuarioDto> response = Assert.IsType<GenericResponse<UsuarioDto>>(createdResult.Value);

            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("Laura", response.Data.Nombres);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedUsuario()
        {
            Guid id = Guid.NewGuid();
            UsuarioDto input = new UsuarioDto
            {
                Nombres = "Pedro",
                Apellidos = "Ramírez",
                Correo = "pedro@example.com",
                Telefono = "000",
                Estado = false,
                Rol = Rol.Admin
            };

            _mockService.Setup(s => s.UpdateAsync(id, input))
                .ReturnsAsync(new GenericResponse<UsuarioDto>
                {
                    StatusCode = 200,
                    Data = input,
                    Message = "Updated"
                });

            IActionResult result = await _controller.Update(id, input);
            ObjectResult okResult = Assert.IsType<ObjectResult>(result);
            GenericResponse<UsuarioDto> response = Assert.IsType<GenericResponse<UsuarioDto>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Pedro", response.Data.Nombres);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            Guid id = Guid.NewGuid();

            _mockService.Setup(s => s.DeleteAsync(id))
                .ReturnsAsync(new GenericResponse<string>
                {
                    StatusCode = 200,
                    Data = "Deleted",
                    Message = "Deleted successfully"
                });

            IActionResult result = await _controller.Delete(id);
            ObjectResult okResult = Assert.IsType<ObjectResult>(result);
            GenericResponse<string> response = Assert.IsType<GenericResponse<string>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Deleted", response.Data);
        }
    }
}
