using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cotizacion;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoContenedorApiController
        : Controllers.Catalogos.TipoContenedorApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoContenedorApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoContenedorResponse MapToResponse(TipoContenedor t)
        {
            return new TipoContenedorResponse
            {
                TipoContenedorId = t.TipoContenedorId,
                Nombre = t.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTipoContenedorApiAsync(string version)
        {
            var tipos = await _context.TipoContenedor
                .AsNoTracking()
                .OrderBy(t => t.TipoContenedorId)
                .ToListAsync();

            return Ok(tipos.Select(MapToResponse));
        }
    }
}
