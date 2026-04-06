using System;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class SiniestrosApiController : Controllers.SiniestrosApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public SiniestrosApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private SiniestrosResponse MapToResponse(Siniestros s)
        {
            return new SiniestrosResponse
            {
                SiniestroId = s.SiniestroId,
                CertificadoId = s.CertificadoId,
                NReporte = s.NReporte,
                FechaApertura = s.FechaApertura,
                FechaCierre = s.FechaCierre,
                TipoSiniestroId = s.TipoSiniestroId,
                Mercancia = s.Mercancia,
                LugarDeSiniestro = s.LugarDeSiniestro,
                SumaAsegurada = s.SumaAsegurada,
                MontoDeReclamo = s.MontoDeReclamo,
                MontoDeIndemnizacion = s.MontoDeIndemnizacion,
                TipoDeEventoId = s.TipoDeEventoId,

                // 🔹 Datos enriquecidos
                NombreTipoSiniestro = s.TipoSiniestro != null
                    ? s.TipoSiniestro.Tipo
                    : null,

                NumeroCertificado = s.Certificado != null
                    ? s.Certificado.CertificadoId.ToString()
                    : null
            };
        }


        private void MapToSiniestros(Siniestros siniestros, SiniestrosRequest body)
        {
            if (siniestros == null || body == null) return;

            siniestros.CertificadoId = body.CertificadoId;
            siniestros.NReporte = body.NReporte;
            siniestros.FechaApertura = body.FechaApertura;
            siniestros.FechaCierre = body.FechaCierre;
            siniestros.TipoSiniestroId = body.TipoSiniestroId;
            siniestros.Mercancia = body.Mercancia;
            siniestros.LugarDeSiniestro = body.LugarDeSiniestro;
            siniestros.SumaAsegurada = body.SumaAsegurada;
            siniestros.MontoDeReclamo = body.MontoDeReclamo;
            siniestros.MontoDeIndemnizacion = body.MontoDeIndemnizacion;
            siniestros.TipoDeEventoId = body.TipoDeEventoId;
        }


        private IQueryable<Siniestros> QuerySiniestrosCompleto()
        {
            return _context.Siniestros
                .Include(x => x.TipoSiniestro)
                .Include(x => x.Certificado);
        }



        public override async Task<IActionResult> GetSiniestrosAsync(string version)
        {
            var siniestros = await QuerySiniestrosCompleto()
                .AsNoTracking()
                .OrderByDescending(s => s.FechaApertura)
                .ToListAsync();

            var response = siniestros
                .Select(s => MapToResponse(s))
                .ToList();

            return Ok(response);
        }

        public override async Task<IActionResult> GetSiniestroByIdAsync(string version, int idSiniestro)
        {
            var siniestro = await QuerySiniestrosCompleto()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SiniestroId == idSiniestro);

            if (siniestro == null)
                return NotFound();

            return Ok(MapToResponse(siniestro));
        }

        public override async Task<IActionResult> CreateSiniestroAsync(string version, [FromBody] SiniestrosRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var siniestro = new Siniestros();

                MapToSiniestros(siniestro, body);

                _context.Siniestros.Add(siniestro);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var siniestroCreado = await QuerySiniestrosCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.SiniestroId == siniestro.SiniestroId);

                return Ok(MapToResponse(siniestroCreado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> UpdateSiniestroAsync(string version, int idSiniestro, [FromBody] SiniestrosRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var siniestro = await _context.Siniestros
                    .FirstOrDefaultAsync(s => s.SiniestroId == idSiniestro);

                if (siniestro == null)
                    return NotFound();

                // 🔹 Mapear cambios
                MapToSiniestros(siniestro, body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var siniestroActualizado = await QuerySiniestrosCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.SiniestroId == idSiniestro);

                return Ok(MapToResponse(siniestroActualizado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> DeleteSiniestroAsync(string version, int idSiniestro)
        {
            var siniestro = await _context.Siniestros
                .FirstOrDefaultAsync(s => s.SiniestroId == idSiniestro);

            if (siniestro == null)
                return NotFound(new { message = "El siniestro no existe" });

            try
            {
                _context.Siniestros.Remove(siniestro);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Siniestro eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el siniestro", detail = ex.Message });
            }
        }

    }
}
