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
    public class EndososApiController : Controllers.EndososApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public EndososApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private EndososResponse MapToResponse(Endosos e)
        {
            return new EndososResponse
            {
                EndosoId = e.EndosoId,
                TipoEndosoId = e.TipoEndosoId,
                NumeroEndoso = e.NumeroEndoso,
                CertificadoId = e.CertificadoId,
                FechaElaboracion = e.FechaElaboracion,
                Agente = e.Agente,
                RFC = e.RFC,
                Oficina = e.Oficina,
                BeneficiarioPreferente = e.BeneficiarioPreferente,
                MonedaId = e.MonedaId,
                IVA = e.IVA,
                TotalAPagar = e.TotalAPagar,
                Descripcion = e.Descripcion,

                // 🔹 Datos enriquecidos
                NombreTipoEndoso = e.TipoEndoso != null
                    ? e.TipoEndoso.Tipo // ⚠️ ajusta si el campo se llama diferente
                    : null,

                NumeroCertificado = e.Certificado != null
                    ? e.Certificado.CertificadoId.ToString() // ⚠️ cambia si tienes folio real
                    : null,
            };
        }


        private void MapToEndosos(Endosos endosos, EndososRequest body)
        {
            if (endosos == null || body == null) return;

            endosos.TipoEndosoId = body.TipoEndosoId;
            endosos.NumeroEndoso = body.NumeroEndoso;
            endosos.CertificadoId = body.CertificadoId;
            endosos.FechaElaboracion = body.FechaElaboracion == default
                ? DateTime.Now
                : body.FechaElaboracion;

            endosos.Agente = body.Agente;
            endosos.RFC = body.RFC;
            endosos.Oficina = body.Oficina;
            endosos.BeneficiarioPreferente = body.BeneficiarioPreferente;
            endosos.MonedaId = body.MonedaId;
            endosos.IVA = body.IVA;
            endosos.TotalAPagar = body.TotalAPagar;
            endosos.Descripcion = body.Descripcion;
        }


        private IQueryable<Endosos> QueryEndososCompleto()
        {
            return _context.Endosos
                .Include(x => x.TipoEndoso)
                .Include(x => x.Certificado);
        }



        public override async Task<IActionResult> GetEndososAsync(string version)
        {
            var endosos = await QueryEndososCompleto()
                .AsNoTracking()
                .OrderByDescending(e => e.FechaElaboracion)
                .ToListAsync();

            var response = endosos
                .Select(e => MapToResponse(e))
                .ToList();

            return Ok(response);
        }

        public override async Task<IActionResult> GetEndosoByIdAsync(string version, int idEndoso)
        {
            var endoso = await QueryEndososCompleto()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EndosoId == idEndoso);

            if (endoso == null)
                return NotFound();

            return Ok(MapToResponse(endoso));
        }

        public override async Task<IActionResult> CreateEndosoAsync(string version, [FromBody] EndososRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var endoso = new Endosos();

                MapToEndosos(endoso, body);

                _context.Endosos.Add(endoso);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var creado = await QueryEndososCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EndosoId == endoso.EndosoId);

                return Ok(MapToResponse(creado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> UpdateEndosoAsync(string version, int idEndoso, [FromBody] EndososRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var endoso = await _context.Endosos
                    .FirstOrDefaultAsync(e => e.EndosoId == idEndoso);

                if (endoso == null)
                    return NotFound();

                MapToEndosos(endoso, body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var actualizado = await QueryEndososCompleto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EndosoId == idEndoso);

                return Ok(MapToResponse(actualizado));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> DeleteEndosoAsync(string version, int idEndoso)
        {
            var endoso = await _context.Endosos
                .FirstOrDefaultAsync(e => e.EndosoId == idEndoso);

            if (endoso == null)
                return NotFound(new { message = "El endoso no existe" });

            try
            {
                _context.Endosos.Remove(endoso);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Endoso eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el endoso", detail = ex.Message });
            }
        }

    }
}
