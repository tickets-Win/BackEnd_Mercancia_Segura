using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoSiniestroApiController
        : Controllers.Catalogos.TipoSiniestroApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoSiniestroApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoSiniestroResponse MapToResponse(TipoSiniestro t)
        {
            return new TipoSiniestroResponse
            {
                TipoSiniestroId = t.TipoSiniestroId,
                Tipo = t.Tipo ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTipoSiniestroApiAsync(string version)
        {
            var tipos = await _context.TipoSiniestro
                .AsNoTracking()
                .OrderBy(t => t.Tipo)
                .ToListAsync();

            return Ok(tipos.Select(MapToResponse));
        }
    }
}
