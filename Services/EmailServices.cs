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
            var client = new SendGridClient(_apiKey);

            var from = new EmailAddress("designerweb1222@gmail.com", "Mi API");

            var subject = request.Asunto;

            var content = $"Nombre: {request.Nombre}\nCorreo: {request.Correo}\nMensaje: {request.Mensaje}";

            var msg = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                PlainTextContent = content
            };

            foreach (var destino in destinos)
            {
                msg.AddTo(new EmailAddress(destino));
            }

            var response = await client.SendEmailAsync(msg);
        }
    }
}
