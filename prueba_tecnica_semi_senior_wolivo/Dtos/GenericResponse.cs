using System.Text.Json.Serialization;

namespace prueba_tecnica_semi_senior_wolivo.Dtos
{
    public class GenericResponse<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }

        public static GenericResponse<T> Success(T data, string? message = null)
        {
            return new GenericResponse<T> { StatusCode = 200, Data = data, Message = message };
        }

        public static GenericResponse<T> Fail(string message, int statusCode = 400)
        {
            return new GenericResponse<T> { StatusCode = statusCode, Message = message };
        }

        public static GenericResponse<T> Fail(List<string> errors, int statusCode = 400)
        {
            return new GenericResponse<T>
            {
                StatusCode = statusCode,
                Message = "Se encontraron errores de validación.",
                Errors = errors
            };
        }
    }
}
