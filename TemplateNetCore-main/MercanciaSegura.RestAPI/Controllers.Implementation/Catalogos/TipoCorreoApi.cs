using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoCorreoApiController : Controllers.Catalogos.TipoCorreoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoCorreoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoCorreoResponse MapToResponse(TipoCorreo t)
        {
            return new TipoCorreoResponse
            {
                TipoCorreoId = t.TipoCorreoId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoCorreoApiAsync(string version)
        {
            var tiposCorreo = await _context.TipoCorreo.ToListAsync();
            return Ok(tiposCorreo.Select(MapToResponse));
        }
    }
}
