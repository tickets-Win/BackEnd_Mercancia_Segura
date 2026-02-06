using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class AseguradoraApiController : Controllers.Catalogos.AseguradoraApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public AseguradoraApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private AseguradoraResponse MapToRequest(Aseguradora a)
        {
            return new AseguradoraResponse
            {
                AseguradoraId = a.AseguradoraId,
                Nombre = a.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetAseguradoraApiAsync(string version)
        {
            var aseguradoras = await _context.Aseguradora.ToListAsync();
            return Ok(aseguradoras.Select(MapToRequest));
        }
    }
}
