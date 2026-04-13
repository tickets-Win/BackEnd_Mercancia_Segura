using System;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class CotizacionApiController : Controllers.CotizacionApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public CotizacionApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private CotizacionResponse MapToResponse(Cotizacion c)
        {
            return new CotizacionResponse
            {
                CotizacionId = c.CotizacionId,
                PolizaId = c.PolizaId,
                NumeroPoliza = c.Poliza != null ? c.Poliza.NumeroPoliza : null,
                FechaCotizacion = c.FechaCotizacion,
                ClienteId = c.ClienteId,
                NombreCliente = c.Cliente != null ? c.Cliente.NombreCompleto : null,
                VigenciaDel = c.VigenciaDel,
                VigenciaHasta = c.VigenciaHasta,
                SumaAsegurada = c.SumaAsegurada,
                GastosExpedicion = c.GastosExpedicion,
                MotivoCancelacion = c.MotivoCancelacion,
                FechaCancelacion = c.FechaCancelacion,

                CotizacionContenedor = c.CotizacionContenedor == null ? null : new CotizacionContenedorResponse
                {
                    CotizacionContenedorId = c.CotizacionContenedor.CotizacionContenedorId,
                    CotizacionId = c.CotizacionContenedor.CotizacionId,
                    TamanoTipoContendor = c.CotizacionContenedor.TamanoTipoContendor,
                    NumeroContenedor = c.CotizacionContenedor.NumeroContenedor,
                    OpcionCuota = c.CotizacionContenedor.OpcionCuota,
                    Tarifa = c.CotizacionContenedor.Tarifa,
                    TotalTarifa = c.CotizacionContenedor.TotalTarifa,
                    IVA = c.CotizacionContenedor.IVA,
                    Total = c.CotizacionContenedor.Total,
                    TotalSeguroContenedor = c.CotizacionContenedor.TotalSeguroContenedor,
                },

                CotizacionMercancia = c.CotizacionMercancia == null ? null : new CotizacionMercanciaResponse
                {
                    CotizacionMercanciaId = c.CotizacionMercancia.CotizacionMercanciaId,
                    CotizacionId = c.CotizacionMercancia.CotizacionId,
                    CotizacionCliente = c.CotizacionMercancia.CotizacionCliente,
                    Transito = c.CotizacionMercancia.Transito,
                    Clasificacion = c.CotizacionMercancia.Clasificacion,
                    SubClasificacion = c.CotizacionMercancia.SubClasificacion,
                    DescripcionMercancia = c.CotizacionMercancia.DescripcionMercancia,
                    TipoEmpaque = c.CotizacionMercancia.TipoEmpaque,
                    Origen = c.CotizacionMercancia.Origen,
                    Destino = c.CotizacionMercancia.Destino,
                    MedioDeConduccion = c.CotizacionMercancia.MedioDeConduccion,
                    MedioDeTransporte = c.CotizacionMercancia.MedioDeTransporte,
                    Observaciones = c.CotizacionMercancia.Observaciones,
                    MedidasDeSeguridadAdicionales = c.CotizacionMercancia.MedidasDeSeguridadAdicionales,
                    Deducibles = c.CotizacionMercancia.Deducibles,
                    CuotaAplicable = c.CotizacionMercancia.CuotaAplicable,
                    CuotaMinima = c.CotizacionMercancia.CuotaMinima,
                    TipoCambioCotizar = c.CotizacionMercancia.TipoCambioCotizar,
                    PrimaServicioDeAseguramiento = c.CotizacionMercancia.PrimaServicioDeAseguramiento,
                    Subtotal = c.CotizacionMercancia.Subtotal,
                    IVA = c.CotizacionMercancia.IVA,
                    TotalSeguroMercancia = c.CotizacionMercancia.TotalSeguroMercancia,
                    Total = c.CotizacionMercancia.Total
                }
            };
        }


        private void MapToCotizacion(Cotizacion cotizacion, CotizacionRequest body)
        {
            if (cotizacion == null || body == null) return;

            cotizacion.PolizaId = body.PolizaId;
            cotizacion.FechaCotizacion = body.FechaCotizacion;
            cotizacion.ClienteId = body.ClienteId;

            cotizacion.VigenciaDel = body.VigenciaDel;
            cotizacion.VigenciaHasta = body.VigenciaHasta;

            cotizacion.SumaAsegurada = body.SumaAsegurada;
            cotizacion.GastosExpedicion = body.GastosExpedicion;

            cotizacion.MotivoCancelacion = body.MotivoCancelacion;

        }
        private void MapToCotizacionMercancia(CotizacionMercancia entity, CotizacionMercanciaRequest body)
        {
            if (entity == null || body == null) return;

            entity.CotizacionCliente = body.CotizacionCliente;
            entity.Transito = body.Transito;
            entity.Clasificacion = body.Clasificacion;
            entity.SubClasificacion = body.SubClasificacion;
            entity.DescripcionMercancia = body.DescripcionMercancia;
            entity.TipoEmpaque = body.TipoEmpaque;

            entity.Origen = body.Origen;
            entity.Destino = body.Destino;

            entity.MedioDeConduccion = body.MedioDeConduccion;
            entity.MedioDeTransporte = body.MedioDeTransporte;

            entity.Observaciones = body.Observaciones;
            entity.MedidasDeSeguridadAdicionales = body.MedidasDeSeguridadAdicionales;

            entity.Deducibles = body.Deducibles;

            entity.CuotaAplicable = body.CuotaAplicable;
            entity.CuotaMinima = body.CuotaMinima;

            entity.TipoCambioCotizar = body.TipoCambioCotizar;

            entity.PrimaServicioDeAseguramiento = body.PrimaServicioDeAseguramiento;

            entity.Subtotal = body.Subtotal;
            entity.IVA = body.IVA;

            entity.TotalSeguroMercancia = body.TotalSeguroMercancia;

            entity.Total = body.Total;
        }
        private void MapToCotizacionContenedor(CotizacionContenedor entity, CotizacionContenedorRequest body)
        {
            if (entity == null || body == null) return;

            entity.TamanoTipoContendor = body.TamanoTipoContendor;
            entity.NumeroContenedor = body.NumeroContenedor;

            entity.OpcionCuota = body.OpcionCuota;
            entity.Tarifa = body.Tarifa;
            entity.TotalTarifa = body.TotalTarifa;

            entity.IVA = body.IVA;
            entity.Total = body.Total;

            entity.TotalSeguroContenedor = body.TotalSeguroContenedor;

        }

        private IQueryable<Cotizacion> QueryCotizacionCompleta()
        {
            return _context.Cotizacion
                .Include(x => x.Poliza)
                .Include(x => x.Cliente)
                .Include(x => x.CotizacionMercancia)
                .Include(x => x.CotizacionContenedor);
        }



        public override async Task<IActionResult> GetCotizacionAsync(string version)
        {
            var cotizaciones = await QueryCotizacionCompleta()
                .AsNoTracking()
                .Where(c => c.FechaCancelacion == null)
                .OrderByDescending(c => c.FechaCotizacion)
                .ToListAsync();

            var response = cotizaciones
                .Select(c => MapToResponse(c))
                .ToList();

            return Ok(response);
        }

        public override async Task<IActionResult> GetCotizacionByIdAsync(string version, int idCotizacion)
        {
            var cotizacion = await QueryCotizacionCompleta()
                .AsNoTracking()
                .Where(c => c.FechaCancelacion == null)
                .FirstOrDefaultAsync(c => c.CotizacionId == idCotizacion);

            if (cotizacion == null)
                return NotFound();

            return Ok(MapToResponse(cotizacion));
        }
        public override async Task<IActionResult> CreateCotizacionAsync(string version, [FromBody] CotizacionRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cotizacion = new Cotizacion
                {
                    FechaCotizacion = DateTime.Now
                };

                MapToCotizacion(cotizacion, body);

                // 🔹 CotizacionContenedor o CotizacionMercancia
                if (body.CotizacionContenedor != null &&
    !string.IsNullOrEmpty(body.CotizacionContenedor.NumeroContenedor))
                {
                    var contenedor = new CotizacionContenedor();

                    MapToCotizacionContenedor(contenedor, body.CotizacionContenedor);

                    cotizacion.CotizacionContenedor = contenedor;
                }
                else if (body.CotizacionMercancia != null &&
                         !string.IsNullOrEmpty(body.CotizacionMercancia.DescripcionMercancia))
                {
                    var mercancia = new CotizacionMercancia();

                    MapToCotizacionMercancia(mercancia, body.CotizacionMercancia);

                    cotizacion.CotizacionMercancia = mercancia;
                }

                _context.Cotizacion.Add(cotizacion);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var cotizacionCreada = await QueryCotizacionCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CotizacionId == cotizacion.CotizacionId);

                return Ok(MapToResponse(cotizacionCreada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public override async Task<IActionResult> UpdateCotizacionAsync(string version, int idCotizacion, [FromBody] CotizacionRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cotizacion = await _context.Cotizacion
                    .Include(c => c.CotizacionContenedor)
                    .Include(c => c.CotizacionMercancia)
                    .FirstOrDefaultAsync(c => c.CotizacionId == idCotizacion);

                if (cotizacion == null)
                    return NotFound();

                if (cotizacion.FechaCancelacion != null)
                    return BadRequest("La cotización está cancelada y no puede modificarse");

                // 🔹 actualizar datos generales
                MapToCotizacion(cotizacion, body);

                // 🔹 Contenedor o Mercancia (solo uno)
                if (body.CotizacionContenedor != null)
                {
                    if (cotizacion.CotizacionContenedor == null)
                    {
                        cotizacion.CotizacionContenedor = new CotizacionContenedor
                        {
                            CotizacionId = cotizacion.CotizacionId
                        };

                        _context.CotizacionContenedor.Add(cotizacion.CotizacionContenedor);
                    }

                    MapToCotizacionContenedor(cotizacion.CotizacionContenedor, body.CotizacionContenedor);

                    // eliminar mercancia si existía
                    if (cotizacion.CotizacionMercancia != null)
                    {
                        _context.CotizacionMercancia.Remove(cotizacion.CotizacionMercancia);
                        cotizacion.CotizacionMercancia = null;
                    }
                }
                else if (body.CotizacionMercancia != null)
                {
                    if (cotizacion.CotizacionMercancia == null)
                    {
                        cotizacion.CotizacionMercancia = new CotizacionMercancia
                        {
                            CotizacionId = cotizacion.CotizacionId
                        };

                        _context.CotizacionMercancia.Add(cotizacion.CotizacionMercancia);
                    }

                    MapToCotizacionMercancia(cotizacion.CotizacionMercancia, body.CotizacionMercancia);

                    // eliminar contenedor si existía
                    if (cotizacion.CotizacionContenedor != null)
                    {
                        _context.CotizacionContenedor.Remove(cotizacion.CotizacionContenedor);
                        cotizacion.CotizacionContenedor = null;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var cotizacionActualizada = await QueryCotizacionCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CotizacionId == idCotizacion);

                return Ok(MapToResponse(cotizacionActualizada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> DeleteCotizacionAsync(string version, int idCotizacion)
        {
            var cotizacion = await _context.Cotizacion
                .FirstOrDefaultAsync(c => c.CotizacionId == idCotizacion);

            if (cotizacion == null)
                return NotFound(new { message = "La cotización no existe" });

            if (cotizacion.FechaCancelacion != null)
                return BadRequest(new { message = "La cotización ya está cancelada" });

            try
            {
                cotizacion.FechaCancelacion = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Cotización cancelada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cancelar la cotización", detail = ex.Message });
            }
        }

    }
}
