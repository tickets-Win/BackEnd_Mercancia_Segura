using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cotizacion;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TransitoApiController : Controllers.Catalogos.TransitoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TransitoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TransitoResponse MapToResponse(Transito t)
        {
            return new TransitoResponse
            {
                TransitoId = t.Transito_Id,
                Nombre = t.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetTransitoApiAsync(string version)
        {
            var transitos = await _context.Transito.ToListAsync();
            return Ok(transitos.Select(MapToResponse));
        }
    }
}