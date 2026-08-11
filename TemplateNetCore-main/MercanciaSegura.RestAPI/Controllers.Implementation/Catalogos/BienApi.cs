using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class BienApiController : Controllers.BienApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public BienApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private BienResponse MapToResponse(Bien b)
        {
            return new BienResponse
            {
                BienId = b.BienId,
                PolizaId = b.PolizaId,
                AdministracionBienId = b.AdministracionBienId,
                TipoBienId = b.TipoBienId,
                NombreTipoBien = b.TipoBien?.Nombre,
                Nombre = b.Nombre
            };
        }

        public override async Task<IActionResult> GetBienByPolizaAsync(string version, int polizaId)
        {
            var bienes = await _context.Bien
                .AsNoTracking()
                .Include(x => x.TipoBien)
                .Where(x => x.PolizaId == polizaId)
                .ToListAsync();

            return Ok(bienes.Select(MapToResponse));
        }
    }
}