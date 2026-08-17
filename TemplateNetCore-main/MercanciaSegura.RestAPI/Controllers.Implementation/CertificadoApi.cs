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

        /// <summary>Estatus con el que nace un certificado. 1 = Activo.</summary>
        private const int EstatusActivo = 1;

        /// <summary>Prefijo de la clave del certificado.</summary>
        private const string PrefijoClave = "CERT-";

        /// <summary>
        /// Clave que se le pone al certificado al crearlo: CERT- más el
        /// consecutivo a cinco dígitos (CERT-00003). Cambiar el formato es
        /// cambiar solo este método.
        /// </summary>
        private static string ArmarClave(int certificadoId)
        {
            return PrefijoClave + certificadoId.ToString("00000");
        }

        private CertificadoResponse MapToResponse(Certificado c)
        {
            return new CertificadoResponse
            {
                CertificadoId = c.CertificadoId,
                CotizacionId = c.CotizacionId,
                ClaveCertificado = c.ClaveCertificado,
                Asegurado = c.Asegurado,
                FechaRegistro = c.FechaRegistro,
                FechaInicio = c.FechaInicio,
                FechaFin = c.FechaFin,
                SumaAsegurada = c.SumaAsegurada,
                TipoEstatusId = c.TipoEstatusId,

                // 🔹 Datos enriquecidos
                NombreEstatus = c.TipoEstatus != null
                    ? c.TipoEstatus.Tipo
                    : null,

                NombreCliente = c.Cotizacion != null && c.Cotizacion.Cliente != null
                    ? c.Cotizacion.Cliente.NombreCompleto
                    : c.Asegurado,

                NumeroPoliza = c.Cotizacion != null && c.Cotizacion.Poliza != null
                    ? c.Cotizacion.Poliza.NumeroPoliza
                    : null,

                FechaCotizacion = c.Cotizacion != null
                    ? c.Cotizacion.FechaCotizacion
                    : (DateTime?)null,

                TipoCotizacion = c.Cotizacion == null
                    ? null
                    : c.Cotizacion.CotizacionMercancia != null
                        ? "Mercancía"
                        : "Contenedor",

                Total = c.Cotizacion != null
                    ? c.Cotizacion.Total
                    : null
            };
        }


        /// <summary>
        /// Vuelca el request sobre la entidad. Lo que el cliente no manda se toma
        /// de la cotización: al confirmarla, el certificado hereda su asegurado,
        /// su vigencia y su suma asegurada.
        /// </summary>
        private void MapToCertificado(Certificado certificado, CertificadoRequest body, DOM.Modelos.Cotizacion.Cotizacion cotizacion)
        {
            if (certificado == null || body == null) return;

            certificado.CotizacionId = body.CotizacionId;

            certificado.FechaRegistro = body.FechaRegistro ?? DateTime.Now;

            certificado.ClaveCertificado = body.ClaveCertificado;

            certificado.Asegurado = !string.IsNullOrWhiteSpace(body.Asegurado)
                ? body.Asegurado
                : cotizacion?.Cliente?.NombreCompleto;

            certificado.FechaInicio = body.FechaInicio
                ?? cotizacion?.VigenciaDel
                ?? certificado.FechaRegistro;

            certificado.FechaFin = body.FechaFin
                ?? cotizacion?.VigenciaHasta
                ?? certificado.FechaInicio;

            // La suma asegurada solo existe en las cotizaciones de mercancía; las
            // de contenedor no la capturan.
            certificado.SumaAsegurada = body.SumaAsegurada
                ?? cotizacion?.CotizacionMercancia?.SumaAsegurada
                ?? 0m;

            certificado.TipoEstatusId = body.TipoEstatusId ?? EstatusActivo;
        }


        /// <summary>Trae la cotización con lo que se necesita para llenar el certificado.</summary>
        private Task<DOM.Modelos.Cotizacion.Cotizacion?> BuscarCotizacionAsync(int cotizacionId)
        {
            return _context.Cotizacion
                .Include(c => c.Cliente)
                .Include(c => c.CotizacionMercancia)
                .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);
        }


        private IQueryable<Certificado> QueryCertificadoCompleto()
        {
            return _context.Certificado
                .Include(x => x.TipoEstatus)
                .Include(x => x.Cotizacion)
                    .ThenInclude(c => c.Cliente)
                .Include(x => x.Cotizacion)
                    .ThenInclude(c => c.Poliza)
                .Include(x => x.Cotizacion)
                    .ThenInclude(c => c.CotizacionMercancia);
        }



        // GET ALL
        public override async Task<IActionResult> GetCertificadosAsync(string version)
        {
            var certificados = await QueryCertificadoCompleto()
                .AsNoTracking()
                .OrderByDescending(c => c.FechaRegistro)
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
                var cotizacion = await BuscarCotizacionAsync(body.CotizacionId);

                if (cotizacion == null)
                    return BadRequest("La cotización no existe");

                // Una cotización no puede certificarse dos veces.
                var yaCertificada = await _context.Certificado
                    .AnyAsync(c => c.CotizacionId == body.CotizacionId);

                if (yaCertificada)
                    return BadRequest("Esta cotización ya tiene certificado");

                var certificado = new Certificado();

                MapToCertificado(certificado, body, cotizacion);

                _context.Certificado.Add(certificado);

                await _context.SaveChangesAsync();

                // La clave se arma con el id, así que hasta aquí no se podía: el
                // consecutivo lo asigna la base al insertar.
                if (string.IsNullOrWhiteSpace(certificado.ClaveCertificado))
                {
                    certificado.ClaveCertificado = ArmarClave(certificado.CertificadoId);

                    await _context.SaveChangesAsync();
                }

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

                var cotizacion = await BuscarCotizacionAsync(body.CotizacionId);

                MapToCertificado(certificado, body, cotizacion);

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

            // Un certificado con siniestros no se puede borrar: la FK lo impediría
            // de todos modos, pero así el mensaje es entendible.
            var tieneSiniestros = await _context.Siniestros
                .AnyAsync(s => s.CertificadoId == idCertificado);

            if (tieneSiniestros)
                return BadRequest(new { message = "No se puede eliminar: el certificado tiene siniestros registrados" });

            var tieneEndosos = await _context.Endosos
                .AnyAsync(e => e.CertificadoId == idCertificado);

            if (tieneEndosos)
                return BadRequest(new { message = "No se puede eliminar: el certificado tiene endosos registrados" });

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
