using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class ClienteVendedorApiController : Controllers.ClienteVendedorApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public ClienteVendedorApiController(ServiceDbContext context)
        {
            _context = context;
        }
        public override async Task<IActionResult> GetVendedoresByClienteAsync(
    string version,
    int clienteId)
        {
            var vendedores = await _context.ClienteVendedor
                .Where(cv => cv.ClienteId == clienteId)
                .Include(cv => cv.Vendedor)
                .Select(cv => new ClienteVendedorResponse
                {
                    Vendedor = cv.Vendedor != null
                        ? cv.Vendedor.NombreCompleto
                        : null,
                    Comision = cv.Comision
                })
                .ToListAsync();

            return Ok(vendedores);
        }

    }
}
