using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class AdministracionBienApiController : Controllers.Catalogos.AdministracionBienApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public AdministracionBienApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private AdministracionBienResponse MapToResponse(AdministracionBien a)
        {
            return new AdministracionBienResponse
            {
                AdministracionBienId = a.AdministracionBienId,
                Nombre = a.Nombre ?? string.Empty
            };
        }

        public override async Task<IActionResult> GetAdministracionBienApiAsync(string version)
        {
            var administraciones = await _context.AdministracionBien.ToListAsync();
            return Ok(administraciones.Select(MapToResponse));
        }
    }
}
