using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoEventoApiController
        : Controllers.Catalogos.TipoEventoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoEventoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoEventoResponse MapToResponse(TipoEvento t)
        {
            return new TipoEventoResponse
            {
                TipoEventoId = t.TipoEventoId,
                Tipo = t.Tipo ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTipoEventoApiAsync(string version)
        {
            // Por id, no por nombre: el orden del catalogo es el que se captura.
            var tipos = await _context.TipoEvento
                .AsNoTracking()
                .OrderBy(t => t.TipoEventoId)
                .ToListAsync();

            return Ok(tipos.Select(MapToResponse));
        }
    }
}
