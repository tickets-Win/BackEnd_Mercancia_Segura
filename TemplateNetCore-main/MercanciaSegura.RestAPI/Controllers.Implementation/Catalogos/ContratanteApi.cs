using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class ContratanteApiController : Controllers.Catalogos.ContratanteApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public ContratanteApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private ContratanteResponse MapToRequest(Contratante c)
        {
            return new ContratanteResponse
            {
                ContratanteId = c.ContratanteId,
                Nombre = c.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetContratanteApiAsync(string version)
        {
            var contratantes = await _context.Contratante.ToListAsync();
            return Ok(contratantes.Select(MapToRequest));
        }
    }
}
