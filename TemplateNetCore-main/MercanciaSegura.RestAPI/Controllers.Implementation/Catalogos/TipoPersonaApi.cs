using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{

    public class TipoPersonaApiController : Controllers.Catalogos.TipoPersonaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoPersonaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoPersonaResponse MapToRequest(TipoPersona t)
        {
            return new TipoPersonaResponse
            {
                TipoPersonaId = t.TipoPersonaId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoPersonaApiAsync(string version)
        {
            var tipoPersona = await _context.TipoPersona.ToListAsync();
            return Ok(tipoPersona.Select(MapToRequest));
        }
    }
}

