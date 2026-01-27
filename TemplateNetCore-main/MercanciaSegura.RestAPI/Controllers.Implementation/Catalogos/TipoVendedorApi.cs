using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Vendedor;
using MercanciaSegura.RestAPI.Controllers.Catalogos;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{

    public class TipoVendedorApiController : Controllers.Catalogos.TipoVendedorApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public TipoVendedorApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private TipoVendedorResponse MapToRequest(TipoVendedor t)
        {
            return new TipoVendedorResponse
            {
                TipoVendedorId = t.TipoVendedorId,
                Tipo = t.Tipo
            };
        }

        public override async Task<IActionResult> GetTipoVendedorApiAsync(string version)
        {
            var tipoVendedor = await _context.TipoVendedor.ToListAsync();
            return Ok(tipoVendedor.Select(MapToRequest));
        }
    }
}

