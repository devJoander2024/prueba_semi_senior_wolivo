using prueba_tecnica_semi_senior_wolivo.Enum;
using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_semi_senior_wolivo.Dtos
{
    public class ActividadDto
    {
        public Guid ActividadId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El título no puede exceder los 255 caracteres.")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public Estado Estado { get; set; }
        public int HorasEstimadas { get; set; }
        public int HorasReales { get; set; }
        
        [Required(ErrorMessage = "El proyecto es obligatorio para crear la actividad.")]
        public Guid ProyectoId { get; set; }

         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HorasEstimadas < 0)
            {
                yield return new ValidationResult(
                    "Las horas estimadas no pueden ser negativas.",
                    new[] { nameof(HorasEstimadas) });
            }

            if (HorasReales < 0)
            {
                yield return new ValidationResult(
                    "Las horas reales no pueden ser negativas.",
                    new[] { nameof(HorasReales) });
            }

            if (HorasEstimadas > HorasReales)
            {
                yield return new ValidationResult(
                    "Las horas estimadas no pueden ser mayores que las reales.",
                    new[] { nameof(HorasEstimadas), nameof(HorasReales) });
            }
        }
    }
}
