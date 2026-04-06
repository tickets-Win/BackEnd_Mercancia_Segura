using System;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class CertificadoApiController : Controllers.CertificadoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public CertificadoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private CertificadoResponse MapToResponse(Certificado c)
        {
            return new CertificadoResponse
            {
                CertificadoId = c.CertificadoId,
                CotizacionId = c.CotizacionId,
                FechaCertificado = c.FechaCertificado,

                // 🔹 Dato enriquecido
                NumeroCotizacion = c.Cotizacion != null
                    ? c.Cotizacion.CotizacionId.ToString() // ⚠️ cambia por Numero si existe
                    : null
            };
        }


        private void MapToCertificado(Certificado certificado, CertificadoRequest body)
        {
            if (certificado == null || body == null) return;

            certificado.CotizacionId = body.CotizacionId;

            certificado.FechaCertificado = body.FechaCertificado == default
                ? DateTime.Now
                : body.FechaCertificado;
        }


        private IQueryable<Certificado> QueryCertificadoCompleto()
        {
            return _context.Certificado
                .Include(x => x.Cotizacion);
        }



        // GET ALL
        public override async Task<IActionResult> GetCertificadosAsync(string version)
        {
            var certificados = await QueryCertificadoCompleto()
                .AsNoTracking()
                .OrderByDescending(c => c.FechaCertificado)
                .ToListAsync();

            var response = certificados
                .Select(c => MapToResponse(c))
                .ToList();

            return Ok(response);
        }

        // GET BY ID
        public override async Task<IActionResult> GetCertificadoByIdAsync(string version, int idCertificado)
        {
            var certificado = await QueryCertificadoCompleto()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CertificadoId == idCertificado);

            if (certificado == null)
                return NotFound();

            return Ok(MapToResponse(certificado));
        }

        // CREATE
        public override async Task<IActionResult> CreateCertificadoAsync(string version, [FromBody] CertificadoRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var certificado = new Certificado();

                MapToCertificado(certificado, body);

                _context.Certificado.Add(certificado);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var creado = await QueryCertificadoCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CertificadoId == certificado.CertificadoId);

                return Ok(MapToResponse(creado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // UPDATE
        public override async Task<IActionResult> UpdateCertificadoAsync(string version, int idCertificado, [FromBody] CertificadoRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var certificado = await _context.Certificado
                    .FirstOrDefaultAsync(c => c.CertificadoId == idCertificado);

                if (certificado == null)
                    return NotFound();

                MapToCertificado(certificado, body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var actualizado = await QueryCertificadoCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CertificadoId == idCertificado);

                return Ok(MapToResponse(actualizado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // DELETE
        public override async Task<IActionResult> DeleteCertificadoAsync(string version, int idCertificado)
        {
            var certificado = await _context.Certificado
                .FirstOrDefaultAsync(c => c.CertificadoId == idCertificado);

            if (certificado == null)
                return NotFound(new { message = "El certificado no existe" });

            try
            {
                _context.Certificado.Remove(certificado);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Certificado eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el certificado", detail = ex.Message });
            }
        }

    }
}
