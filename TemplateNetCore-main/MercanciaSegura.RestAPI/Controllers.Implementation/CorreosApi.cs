using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class CorreosApiController : Controllers.CorreosApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public CorreosApiController(ServiceDbContext context)
        {
            _context = context;
        }
        public override async Task<IActionResult> GetCorreosByClienteAsync(
            string version,
            int clienteId)
        {
            var correos = await _context.Correos
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.TipoCorreo)
                .Select(c => new CorreosResponse
                {
                    CorreoId = c.CorreoId,
                    Correo = c.Correo,
                    TipoCorreo = c.TipoCorreo != null
                        ? c.TipoCorreo.Tipo
                        : null
                })
                .ToListAsync();

            return Ok(correos);
        }
    }
}
