namespace ApiCorreos.Models
{
    public class EmailRequest
    {
        public string IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Numero { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}
