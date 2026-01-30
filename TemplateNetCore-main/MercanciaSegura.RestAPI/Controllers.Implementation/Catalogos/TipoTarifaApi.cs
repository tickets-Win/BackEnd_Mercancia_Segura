using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoTarifaApiController : Controllers.Catalogos.TipoTarifaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoTarifaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoTarifaResponse MapToRequest(TipoTarifa t)
        {
            return new TipoTarifaResponse
            {
                TipoTarifaId = t.TipoTarifaId,
                Tarifa = t.Tarifa
            };
        }

        public override async Task<IActionResult> GetTipoTarifaApiAsync(string version)
        {
            var TipoTarifa = await _context.TipoTarifa.ToListAsync();
            return Ok(TipoTarifa.Select(MapToRequest));
        }
    }
}
