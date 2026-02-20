using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoBienApiController : Controllers.Catalogos.TipoBienApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoBienApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoBienResponse MapToResponse(TipoBien t)
        {
            return new TipoBienResponse
            {
                TipoBienId = t.TipoBienId,
                Nombre = t.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTipoBienApiAsync(string version)
        {
            var tiposBien = await _context.TipoBien.ToListAsync();
            return Ok(tiposBien.Select(MapToResponse));
        }
    }
}
