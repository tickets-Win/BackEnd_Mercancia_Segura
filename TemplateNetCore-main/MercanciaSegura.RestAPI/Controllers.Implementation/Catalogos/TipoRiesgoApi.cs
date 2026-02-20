using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoRiesgoApiController : Controllers.Catalogos.TipoRiesgoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoRiesgoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoRiesgoResponse MapToResponse(TipoRiesgo t)
        {
            return new TipoRiesgoResponse
            {
                TipoRiesgoId = t.TipoRiesgoId,
                Nombre = t.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTipoRiesgoApiAsync(string version)
        {
            var tiposRiesgo = await _context.TipoRiesgo.ToListAsync();
            return Ok(tiposRiesgo.Select(MapToResponse));
        }
    }
}
