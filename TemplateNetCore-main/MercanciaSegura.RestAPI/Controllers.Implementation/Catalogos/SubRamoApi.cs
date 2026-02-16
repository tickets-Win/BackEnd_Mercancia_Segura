using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class SubRamoApiController : Controllers.Catalogos.SubRamoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public SubRamoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private SubRamoResponse MapToResponse(SubRamo sr)
        {
            return new SubRamoResponse
            {
                SubRamoId = sr.SubRamoId,
                Nombre = sr.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetSubRamoApiAsync(string version)
        {
            var subramos = await _context.SubRamo.ToListAsync();
            return Ok(subramos.Select(MapToResponse));
        }
    }
}
