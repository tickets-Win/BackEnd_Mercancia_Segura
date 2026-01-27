using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class RegimenFiscalApiController : Controllers.Catalogos.RegimenFiscalApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public RegimenFiscalApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private RegimenFiscalResponse MapToRequest(RegimenFiscal r)
        {
            return new RegimenFiscalResponse
            {
                RegimenFiscalId = r.RegimenFiscalId,
                AplicaFisica = r.AplicaFisica,
                AplicaMoral = r.AplicaMoral,
                Codigo = r.Codigo,
                Descripcion = r.Descripcion,
            };
        }

        public override async Task<IActionResult> GetRegimenFiscalApiAsync(string version)
        {
            var regimenFiscal = await _context.RegimenFiscal.ToListAsync();
            return Ok(regimenFiscal.Select(MapToRequest));
        }
    }
}
