using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class MonedaApiController
        : Controllers.Catalogos.MonedaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public MonedaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private MonedaResponse MapToRequest(Moneda m)
        {
            return new MonedaResponse
            {
                MonedaId = m.MonedaId,
                Nombre = m.Nombre ?? string.Empty,
                TipoCambio = m.TipoCambio ?? 0,
                TipoCambioVentanilla = m.TipoCambioVentanilla ?? 0
            };
        }

        public override async Task<IActionResult> GetMonedaApiAsync(string version)
        {
            var monedas = await _context.Moneda.ToListAsync();
            return Ok(monedas.Select(MapToRequest));
        }
    }
}
