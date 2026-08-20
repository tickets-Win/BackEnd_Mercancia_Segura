using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cotizacion;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TamanioContenedorApiController
        : Controllers.Catalogos.TamanioContenedorApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TamanioContenedorApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TamanioContenedorResponse MapToResponse(TamanioContenedor t)
        {
            return new TamanioContenedorResponse
            {
                TamanioContenedorId = t.TamanioContenedorId,
                Nombre = t.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTamanioContenedorApiAsync(string version)
        {
            var tamanios = await _context.TamanioContenedor
                .AsNoTracking()
                .OrderBy(t => t.TamanioContenedorId)
                .ToListAsync();

            return Ok(tamanios.Select(MapToResponse));
        }
    }
}
