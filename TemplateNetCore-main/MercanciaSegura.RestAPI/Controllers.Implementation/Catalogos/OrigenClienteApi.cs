using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class OrigenClienteApiController : Controllers.Catalogos.OrigenClienteApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public OrigenClienteApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private OrigenClienteResponse MapToRequest(OrigenCliente o)
        {
            return new OrigenClienteResponse
            {
                OrigenClienteId = o.OrigenClienteId,
                Tipo = o.Tipo
            };
        }

        public override async Task<IActionResult> GetOrigenClienteApiAsync(string version)
        {
            var origenCliente = await _context.OrigenCliente.ToListAsync();
            return Ok(origenCliente.Select(MapToRequest));
        }
    }
}
