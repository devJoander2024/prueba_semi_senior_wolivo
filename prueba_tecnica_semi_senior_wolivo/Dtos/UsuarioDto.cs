using prueba_tecnica_semi_senior_wolivo.Enum;
using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_semi_senior_wolivo.Dtos
{
    public class UsuarioDto
    {
        public Guid UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public Rol Rol { get; set; }
        public bool Estado { get; set; }
    }

}
