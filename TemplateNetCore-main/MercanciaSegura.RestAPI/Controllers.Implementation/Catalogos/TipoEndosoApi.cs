using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class TipoEndosoApiController
        : Controllers.Catalogos.TipoEndosoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoEndosoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        public override async Task<IActionResult> GetTipoEndosoApiAsync(string version)
        {
            var tipos = await _context.TipoEndoso
                .AsNoTracking()
                .OrderBy(t => t.Tipo)
                .ToListAsync();

            var response = tipos.Select(t => new TipoEndosoResponse
            {
                // La llave de la tabla se llama Tipo_ID, no Tipo_Endoso_ID.
                TipoEndosoId = t.TipoId,
                Tipo = t.Tipo ?? string.Empty
            }).ToList();

            return Ok(response);
        }
    }
}
