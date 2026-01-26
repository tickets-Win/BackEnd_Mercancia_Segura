using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MercanciaSegura.DOM.Modelos.Cliente;


namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoEstatusApiController : Controllers.Catalogos.TipoEstatusApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoEstatusApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoEstatusResponse MapToRequest(TipoEstatus t)
        {
            return new TipoEstatusResponse
            {
                EstatusId = t.EstatusId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoEstatusApiAsync(string version)
        {
            var tipoEstatus = await _context.TipoEstatus.ToListAsync();
            return Ok(tipoEstatus.Select(MapToRequest));
        }
    }
}
