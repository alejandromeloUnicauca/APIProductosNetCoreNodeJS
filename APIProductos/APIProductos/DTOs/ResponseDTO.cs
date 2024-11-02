namespace APIProductos.DTOs
{
    public class MensajesResponse
    {
        public const string Success = "Operación exitosa";
        public const string Error = "Error en el servidor";
        public const string NotFound = "Recurso no encontrado";
    }

    public class ResponseDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public object? data {  get; set; }
    }
}
