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

        /// <summary>
        /// Actualiza el tipo de cambio de una moneda. Se dejan pasar solo valores
        /// positivos: un tipo de cambio en cero o negativo descuadraría cualquier
        /// cotización que lo use.
        /// </summary>
        public override async Task<IActionResult> UpdateTipoCambioAsync(
            string version, int idMoneda, [FromBody] Models.TipoCambioRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moneda = await _context.Moneda
                .FirstOrDefaultAsync(m => m.MonedaId == idMoneda);

            if (moneda == null)
                return NotFound(new { message = "La moneda no existe" });

            if (body.TipoCambio.HasValue && body.TipoCambio.Value <= 0)
                return BadRequest("El tipo de cambio debe ser mayor a cero");

            if (body.TipoCambioVentanilla.HasValue && body.TipoCambioVentanilla.Value <= 0)
                return BadRequest("El tipo de cambio bancario debe ser mayor a cero");

            // Lo que no venga en el cuerpo se deja como estaba.
            if (body.TipoCambio.HasValue) moneda.TipoCambio = body.TipoCambio;
            if (body.TipoCambioVentanilla.HasValue) moneda.TipoCambioVentanilla = body.TipoCambioVentanilla;

            await _context.SaveChangesAsync();

            return Ok(MapToRequest(moneda));
        }
    }
}
