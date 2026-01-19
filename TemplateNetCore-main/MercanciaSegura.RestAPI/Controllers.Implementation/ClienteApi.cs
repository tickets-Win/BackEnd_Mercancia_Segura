using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class ClienteApi : Controllers.ClienteApi
    {
        private readonly ClienteService _clienteService;

        public ClienteApi(ClienteService service)
        {
            _clienteservice = clienteservice;
        }

        public override async Task<IActionResult> CrearCliente(
            string version,
            CreateClienteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = await _service.CrearClienteAsync(request);

            return CreatedAtAction(
                nameof(CrearCliente),
                new { id = cliente.ClienteId },
                cliente);
        }
    }

}
