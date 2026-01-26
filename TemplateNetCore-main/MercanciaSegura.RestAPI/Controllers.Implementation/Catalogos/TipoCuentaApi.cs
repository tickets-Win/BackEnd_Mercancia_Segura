using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoCuentaApiController : Controllers.Catalogos.TipoCuentaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoCuentaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoCuentaResponse MapToRequest(TipoCuenta t)
        {
            return new TipoCuentaResponse
            {
                TipoCuentaId = t.TipoCuentaId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoCuentaApiAsync(string version)
        {
            var tipoCuenta = await _context.TipoCuenta.ToListAsync();
            return Ok(tipoCuenta.Select(MapToRequest));
        }
    }
}
