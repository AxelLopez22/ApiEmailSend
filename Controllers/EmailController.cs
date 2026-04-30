using ApiCorreos.Models;
using ApiCorreos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiCorreos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailServices _emailService;
        private readonly List<ClienteConfig> _clientes;

        public EmailController(
            EmailServices emailService,
            IOptions<List<ClienteConfig>> clientesOptions)
        {
            _emailService = emailService;
            _clientes = clientesOptions.Value;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Enviar([FromBody] EmailRequest request)
        {
            var cliente = _clientes.FirstOrDefault(c => c.ClienteId == request.IdCliente);

            if (cliente == null)
                return BadRequest("Cliente no válido");

            await _emailService.EnviarCorreo(cliente.CorreosDestino, request);

            return Ok("Correo enviado");
        }
    }
}
