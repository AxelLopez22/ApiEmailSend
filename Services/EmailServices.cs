using ApiCorreos.Models;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApiCorreos.Services
{
    public class EmailServices
    {
        public async Task EnviarCorreo(List<string> destinos, EmailRequest request)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
        
                var from = new EmailAddress("designerweb1222@gmail.com", "Mi API");
        
                var subject = $"Nuevo mensaje de {request.Nombre}";
        
                var content = $"Nombre: {request.Nombre}\nCorreo: {request.Correo}\nMensaje: {request.Mensaje}";
        
                var htmlContent = $@"
                <div style='font-family: Arial, sans-serif; background-color:#f4f6f8; padding:20px;'>
                    <div style='max-width:600px; margin:auto; background:white; border-radius:10px; padding:20px; box-shadow:0 2px 8px rgba(0,0,0,0.1);'>
                        
                        <h2 style='color:#2c3e50; margin-bottom:10px;'>📩 Nuevo mensaje recibido</h2>
                        <p style='color:#555;'>Has recibido un nuevo mensaje desde tu formulario web.</p>
        
                        <hr style='border:none; border-top:1px solid #eee; margin:20px 0;' />
        
                        <p><strong>👤 Nombre:</strong><br /> {request.Nombre}</p>
                        
                        <p><strong>📧 Correo:</strong><br /> 
                        <a href='mailto:{request.Correo}' style='color:#3498db; text-decoration:none;'>
                            {request.Correo}
                        </a></p>
        
                        <p><strong>💬 Mensaje:</strong><br />
                        <span style='display:block; background:#f9f9f9; padding:10px; border-radius:5px; white-space:pre-line;'>
                            {request.Mensaje}
                        </span>
                        </p>
        
                        <hr style='border:none; border-top:1px solid #eee; margin:20px 0;' />
        
                        <p style='font-size:12px; color:#999; text-align:center;'>
                            Este mensaje fue enviado automáticamente desde tu sistema.
                        </p>
        
                    </div>
                </div>";
        
                var msg = new SendGridMessage()
                {
                    From = from,
                    Subject = subject,
                    PlainTextContent = content,
                    HtmlContent = htmlContent
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
