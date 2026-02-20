using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class EstatusPolizaApiController : Controllers.Catalogos.EstatusPolizaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public EstatusPolizaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private EstatusPolizaResponse MapToResponse(EstatusPoliza e)
        {
            return new EstatusPolizaResponse
            {
                EstatusPolizaId = e.EstatusPolizaId,
                Tipo = e.Tipo ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetEstatusPolizaApiAsync(string version)
        {
            var estatusPolizas = await _context.EstatusPoliza.ToListAsync();
            return Ok(estatusPolizas.Select(MapToResponse));
        }
    }
}
