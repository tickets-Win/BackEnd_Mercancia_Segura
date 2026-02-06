using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class FormaPagoApiController : Controllers.Catalogos.FormaPagoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public FormaPagoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private FormaPagoResponse MapToRequest(FormaPago f)
        {
            return new FormaPagoResponse
            {
                FormaPagoId = f.FormaPagoId,
                Nombre = f.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetFormaPagoApiAsync(string version)
        {
            var formasPago = await _context.FormaPago.ToListAsync();
            return Ok(formasPago.Select(MapToRequest));
        }
    }
}
