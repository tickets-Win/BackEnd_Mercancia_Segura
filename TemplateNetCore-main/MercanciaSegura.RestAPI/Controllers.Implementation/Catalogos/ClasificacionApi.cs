using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cotizacion;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class ClasificacionApiController : Controllers.Catalogos.ClasificacionApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public ClasificacionApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private ClasificacionResponse MapToResponse(Clasificacion c)
        {
            return new ClasificacionResponse
            {
                ClasificacionId = c.Clasificacion_Id,
                Nombre = c.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetClasificacionApiAsync(string version)
        {
            var clasificaciones = await _context.Clasificacion.ToListAsync();
            return Ok(clasificaciones.Select(MapToResponse));
        }
    }
}