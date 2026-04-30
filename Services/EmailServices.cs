using ApiCorreos.Models;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApiCorreos.Services
{
    public class EmailServices
    {

        private readonly string _apiKey = "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";
        public async Task EnviarCorreo(List<string> destinos, EmailRequest request) 
        {
            try
            {
                var client = new SendGridClient(_apiKey);
    
                var from = new EmailAddress("designerweb1222@gmail.com", "Mi API");
    
                var subject = request.Asunto;
    
                var content = $"Nombre: {request.Nombre}\nCorreo: {request.Correo}\nMensaje: {request.Mensaje}";
    
                var msg = new SendGridMessage()
                {
                    From = from,
                    Subject = subject,
                    PlainTextContent = content,
                    HtmlContent = $"<p>{content}</p>"
                };
    
                foreach (var destino in destinos)
                {
                    msg.AddTo(new EmailAddress(destino.Trim()));
                }
    
                Console.WriteLine("ANTES DE ENVIAR");
    
                var response = await client.SendEmailAsync(msg);
                var body = await response.Body.ReadAsStringAsync();
    
                Console.WriteLine($"STATUS: {response.StatusCode}");
                Console.WriteLine($"BODY: {body}");
    
                Console.WriteLine("DESPUÉS DE ENVIAR");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR AL ENVIAR CORREO:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
