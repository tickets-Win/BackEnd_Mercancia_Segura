using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class RfcGenericoApiController : Controllers.Catalogos.RfcGenericoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public RfcGenericoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private RfcGenericoResponse MapToRequest(RfcGenerico r)
        {
            return new RfcGenericoResponse
            {
                RfcGenericoId = r.RfcGenericoId,
                Tipo = r.Tipo
            };
        }

        public override async Task<IActionResult> GetRfcGenericoApiAsync(string version)
        {
            var rfcGenerico = await _context.RfcGenerico.ToListAsync();
            return Ok(rfcGenerico.Select(MapToRequest));
        }
    }
}
