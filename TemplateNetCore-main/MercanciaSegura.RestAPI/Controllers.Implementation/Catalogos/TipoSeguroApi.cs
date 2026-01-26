using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Controllers.Catalogos;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{

    public class TipoSeguroApiController : Controllers.Catalogos.TipoSeguroApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoSeguroApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoSeguroResponse MapToRequest(TipoSeguro t)
        {
            return new TipoSeguroResponse
            {
                TipoSeguroId = t.TipoSeguroId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoSeguroApiAsync(string version)
        {
            var tipoSeguro = await _context.TipoSeguro.ToListAsync();
            return Ok(tipoSeguro.Select(MapToRequest));
        }
    }
}

