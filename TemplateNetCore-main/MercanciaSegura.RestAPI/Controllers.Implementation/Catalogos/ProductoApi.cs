using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation.Catalogos
{
    public class ProductoApiController : Controllers.Catalogos.ProductoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public ProductoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private ProductoResponse MapToResponse(Producto p)
        {
            return new ProductoResponse
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre ?? string.Empty,
                Pantalla = p.Pantalla
            };
        }

        public override async Task<IActionResult> GetProductoApiAsync(string version)
        {
            var productos = await _context.Producto.ToListAsync();
            return Ok(productos.Select(MapToResponse));
        }
    }
}
