using prueba_tecnica_semi_senior_wolivo.Enum;
using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_semi_senior_wolivo.Dtos
{
    public class ProyectoDto
    {
        public Guid ProyectoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "El usuario del proyecto es obligatorio.")]
        public Guid UsuarioId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaInicio > FechaFin)
            {
                yield return new ValidationResult(
                    "La fecha de inicio no puede ser mayor que la fecha de fin.",
                    new[] { nameof(FechaInicio), nameof(FechaFin) });
            }
        }
    }
}
