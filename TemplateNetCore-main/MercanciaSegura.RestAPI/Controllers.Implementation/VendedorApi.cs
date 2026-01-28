using System;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Vendedor;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class VendedorApiController : Controllers.VendedorApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public VendedorApiController(ServiceDbContext context)
        {
            _context = context;
        }

        // Mapear Vendedor → VendedorRequest
        private VendedorResponse MapToResponse(Vendedor v)
        {
            return new VendedorResponse
            {   
                VendedorId = v.VendedorId,
                ApellidoPaterno = v.ApellidoPaterno,
                ApellidoMaterno = v.ApellidoMaterno,
                Nombres = v.Nombres,
                NombreCompleto = v.NombreCompleto,
                TipoPersonaId = v.TipoPersonaId,
                TipoVendedorId = v.TipoVendedorId,
                Estatus = v.Estatus,
                Clave = v.Clave,
                Rfc = v.Rfc,
                Domicilio = v.Domicilio,
                Cp = v.Cp,
                Colonia = v.Colonia,
                Estado = v.Estado,
                Genero = v.Genero,
                Telefono = v.Telefono,
                CorreoElectronico = v.CorreoElectronico,
                Observaciones = v.Observaciones,
                Comision = v.Comision,
                FechaRegistro = v.FechaRegistro,
                FechaBaja = v.FechaBaja,
            };
        }

        // Mapear VendedorRequest → Vendedor
        private void MapToVendedor(Vendedor vendedor, VendedorRequest body)
        {
            vendedor.ApellidoPaterno = body.ApellidoPaterno;
            vendedor.ApellidoMaterno = body.ApellidoMaterno;
            vendedor.Nombres = body.Nombres;

            vendedor.NombreCompleto = body.NombreCompleto;

            vendedor.TipoPersonaId = body.TipoPersonaId;
            vendedor.TipoVendedorId = body.TipoVendedorId;
            vendedor.Estatus = body.Estatus;
            vendedor.Clave = body.Clave;
            vendedor.Rfc = body.Rfc;
            vendedor.Domicilio = body.Domicilio;
            vendedor.Cp = body.Cp;
            vendedor.Colonia = body.Colonia;
            vendedor.Estado = body.Estado;
            vendedor.Genero = body.Genero;
            vendedor.Telefono = body.Telefono;
            vendedor.CorreoElectronico = body.CorreoElectronico;
            vendedor.Observaciones = body.Observaciones;
            vendedor.Comision = body.Comision;

            vendedor.FechaActualizacion = DateTime.Now;
        }

        // GET /v1/vendedor
        public override async Task<IActionResult> GetVendedoresAsync(string version)
        {
            var vendedores = await _context.Vendedor
                .Where(v => !v.FechaBaja.HasValue)
                .ToListAsync();

            return Ok(vendedores.Select(MapToResponse));
        }


        // GET /v1/vendedor/{id}
        public override async Task<IActionResult> GetVendedorByIdAsync(string version, int idVendedor)
        {
            var vendedor = await _context.Vendedor
                .FirstOrDefaultAsync(v =>
                    v.VendedorId == idVendedor &&
                    !v.FechaBaja.HasValue);

            if (vendedor == null)
                return NotFound();

            return Ok(MapToResponse(vendedor));
        }


        // POST /v1/vendedor
        public override async Task<IActionResult> CreateVendedorAsync(string version, [FromBody] VendedorRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendedor = new Vendedor
            {
                FechaRegistro = DateTime.Now
            };

            MapToVendedor(vendedor, body);

            _context.Vendedor.Add(vendedor);
            await _context.SaveChangesAsync();

            return Ok(MapToResponse(vendedor));
        }

        // PUT /v1/vendedor/{id}
        public override async Task<IActionResult> UpdateVendedorAsync(string version, int idVendedor, [FromBody] VendedorRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendedor = await _context.Vendedor
                .FirstOrDefaultAsync(v => v.VendedorId == idVendedor);

            if (vendedor == null)
                return NotFound();

            MapToVendedor(vendedor, body);
            await _context.SaveChangesAsync();

            return Ok(MapToResponse(vendedor));
        }


        public override async Task<IActionResult> DeleteVendedorAsync(string version, int idVendedor)
{
    var vendedor = await _context.Vendedor
        .FirstOrDefaultAsync(v => v.VendedorId == idVendedor);

    if (vendedor == null)
        return NotFound();

            if (vendedor.FechaBaja.HasValue)
                return BadRequest(new { message = "El vendedor ya está dado de baja" });

    vendedor.FechaBaja = DateTime.Now;
    vendedor.Estatus = false;

    _context.Entry(vendedor).Property(v => v.FechaBaja).IsModified = true;

    await _context.SaveChangesAsync();

    return Ok(new { message = "El vendedor ya fue dado de baja" });
        }


    }
}
