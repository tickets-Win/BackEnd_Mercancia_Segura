using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cotizacion;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class CotizacionApiController : Controllers.CotizacionApiControllerBase
    {
        private readonly ServiceDbContext _context;

        private const decimal IvaPorcentaje = 0.16m;

        /// <summary>
        /// Subtotal = prima del servicio de aseguramiento + gastos de expedición.
        /// Mismo criterio para mercancía y contenedor, y el mismo que usa el
        /// formulario de WebAdmin para mostrar los importes en pantalla.
        /// </summary>
        private static void CalcularImportes(Cotizacion cotizacion)
        {
            var subtotal = (cotizacion.PrimaServicioDeAseguramiento ?? 0)
                         + (cotizacion.GastosExpedicion ?? 0);

            cotizacion.Subtotal = subtotal;
            cotizacion.IVA = subtotal * IvaPorcentaje;
            cotizacion.Total = subtotal + cotizacion.IVA;
        }

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
                NumeroPoliza = c.Poliza?.NumeroPoliza,

                FechaCotizacion = c.FechaCotizacion,

                ClienteId = c.ClienteId,
                NombreCliente = c.Cliente?.NombreCompleto,

                BeneficiarioPreferenteId = c.BeneficiarioPreferenteId,
                NombreBeneficiario = c.BeneficiarioPreferente != null
                    ? c.BeneficiarioPreferente.NombreCompleto
                    : null,

                MonedaId = c.MonedaId,
                MonedaNombre = c.Moneda?.Nombre,

                VigenciaDel = c.VigenciaDel,
                VigenciaHasta = c.VigenciaHasta,

                FechaCancelacion = c.FechaCancelacion,

                PrimaServicioDeAseguramiento = c.PrimaServicioDeAseguramiento,
                Subtotal = c.Subtotal,
                IVA = c.IVA,
                Total = c.Total,

                GastosExpedicion = c.GastosExpedicion,

                CotizacionMercancia = c.CotizacionMercancia != null
                    ? MapMercancia(c.CotizacionMercancia)
                    : null,

                CotizacionContenedor = c.CotizacionContenedor != null
                    ? c.CotizacionContenedor.Select(MapContenedor).ToList()
                    : null,

                BienCotizacion = c.BienCotizacion != null
                    ? c.BienCotizacion.Select(MapBien).ToList()
                    : null,

                CoberturaCotizacion = c.CoberturaCotizacion != null
                    ? c.CoberturaCotizacion.Select(MapCobertura).ToList()
                    : null
            };
        }

        private CotizacionMercanciaResponse MapMercancia(CotizacionMercancia m)
        {
            return new CotizacionMercanciaResponse
            {
                CotizacionMercanciaId = m.CotizacionMercanciaId,
                CotizacionId = m.CotizacionId,

                CotizacionCliente = m.CotizacionCliente,
                TransitoId = m.TransitoId,

                ClasificacionId = m.ClasificacionId,
                ClasificacionNombre = m.Clasificacion?.Nombre,

                SubClasificacion = m.SubClasificacion,
                DescripcionMercancia = m.DescripcionMercancia,
                TipoEmpaque = m.TipoEmpaque,

                Origen = m.Origen,
                Destino = m.Destino,

                MedioDeConduccion = m.MedioDeConduccion,
                MedioDeTransporte = m.MedioDeTransporte,

                Observaciones = m.Observaciones,
                MedidasDeSeguridadAdicionales = m.MedidasDeSeguridadAdicionales,
                Deducibles = m.Deducibles,

                MonedaCuotaAplicableId = m.MonedaCuotaAplicableId,
                CuotaAplicable = m.CuotaAplicable,

                MonedaCuotaMinimaId = m.MonedaCuotaMinimaId,
                CuotaMinima = m.CuotaMinima,

                TipoCambioCotizar = m.TipoCambioCotizar,
                MonedaCotizarId = m.MonedaCotizarId,

                SumaAsegurada = m.SumaAsegurada
            };
        }

        private CotizacionContenedorResponse MapContenedor(CotizacionContenedor c)
        {
            return new CotizacionContenedorResponse
            {
                CotizacionContenedorId = c.CotizacionContenedorId,
                CotizacionId = c.CotizacionId,

                TamanioContendorId = c.TamanioContendorId,
                TamanioContenedorNombre = c.TamanioContenedor?.Nombre,

                TipoContenedorId = c.TipoContenedorId,
                TipoContenedorNombre = c.TipoContenedor?.Nombre,

                NumeroContenedor = c.NumeroContenedor,

                Cuota = c.Cuota,
                LR = c.LR,
                Referencia = c.Referencia,
                TC = c.TC,

                PrimaUnitariaUSD = c.PrimaUnitariaUSD,
                PrimaUnitariaMXN = c.PrimaUnitariaMXN,

                Total = c.Total
            };
        }

        private BienCotizacionResponse MapBien(BienCotizacion b)
        {
            return new BienCotizacionResponse
            {
                BienCotizacionId = b.BienCotizacionId,
                CotizacionId = b.CotizacionId,
                AdministracionBienId = b.AdministracionBienId,
                TipoBienId = b.TipoBienId,
                NombreTipoBien = b.TipoBien?.Nombre,
                Nombre = b.Nombre
            };
        }


        private void MapToCotizacion(Cotizacion cotizacion, CotizacionRequest body)
        {
            if (cotizacion == null || body == null) return;

            cotizacion.PolizaId = body.PolizaId;
            cotizacion.FechaCotizacion = body.FechaCotizacion;
            cotizacion.ClienteId = body.ClienteId;

            cotizacion.BeneficiarioPreferenteId = body.BeneficiarioPreferenteId;
            cotizacion.MonedaId = body.MonedaId;

            cotizacion.VigenciaDel = body.VigenciaDel;
            cotizacion.VigenciaHasta = body.VigenciaHasta;
            cotizacion.GastosExpedicion = body.GastosExpedicion;
            cotizacion.PrimaServicioDeAseguramiento = body.PrimaServicioDeAseguramiento;
            cotizacion.Subtotal = body.Subtotal;
            cotizacion.IVA = body.IVA;
            cotizacion.Total = body.Total;

        }
        private void MapToCotizacionMercancia(CotizacionMercancia entity, CotizacionMercanciaRequest body)
        {
            if (entity == null || body == null) return;

            entity.CotizacionCliente = body.CotizacionCliente;
            entity.TransitoId = body.TransitoId;

            entity.ClasificacionId = body.ClasificacionId;

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

            entity.MonedaCuotaAplicableId = body.MonedaCuotaAplicableId;
            entity.CuotaAplicable = body.CuotaAplicable;

            entity.MonedaCuotaMinimaId = body.MonedaCuotaMinimaId;
            entity.CuotaMinima = body.CuotaMinima;

            entity.TipoCambioCotizar = body.TipoCambioCotizar;
            entity.MonedaCotizarId = body.MonedaCotizarId;

            entity.SumaAsegurada = body.SumaAsegurada;
        }
        private void MapToCotizacionContenedor(CotizacionContenedor entity, CotizacionContenedorRequest body)
        {
            if (entity == null || body == null) return;

            entity.TamanioContendorId = body.TamanioContendorId;
            entity.TipoContenedorId = body.TipoContenedorId;

            entity.NumeroContenedor = body.NumeroContenedor;

            entity.Cuota = body.Cuota;
            entity.LR = body.LR;

            entity.Referencia = body.Referencia;

            entity.TC = body.TC;

            entity.PrimaUnitariaUSD = body.PrimaUnitariaUSD;
            entity.PrimaUnitariaMXN = body.PrimaUnitariaMXN;

            entity.Total = body.Total;
        }

        private CoberturaCotizacionResponse MapCobertura(CoberturaCotizacion c)
        {
            return new CoberturaCotizacionResponse
            {
                CoberturaCotizacionId = c.CoberturaCotizacionId,
                CotizacionId = c.CotizacionId,
                Nombre = c.Nombre
            };
        }

        private void MapToCobertura(CoberturaCotizacion entity, CoberturaCotizacionRequest body)
        {
            if (entity == null || body == null) return;

            entity.Nombre = body.Nombre;
        }

        private void MapToBien(BienCotizacion entity, BienCotizacionRequest body)
        {
            if (entity == null || body == null) return;

            entity.TipoBienId = body.TipoBienId;
            entity.AdministracionBienId = body.AdministracionBienId;
            entity.Nombre = body.Nombre;
        }

        private IQueryable<Cotizacion> QueryCotizacionCompleta()
        {
            return _context.Cotizacion
                .Include(x => x.Poliza)
                .Include(x => x.Cliente)
                .Include(x => x.Moneda)
                .Include(x => x.BeneficiarioPreferente)

                .Include(x => x.CotizacionMercancia)
                    .ThenInclude(m => m.Clasificacion)

                .Include(x => x.CotizacionContenedor)
                    .ThenInclude(c => c.TipoContenedor)

                .Include(x => x.CotizacionContenedor)
                    .ThenInclude(c => c.TamanioContenedor)

                .Include(x => x.BienCotizacion)
                    .ThenInclude(b => b.TipoBien)

                .Include(x => x.CoberturaCotizacion);
        }

        public override async Task<IActionResult> GetCotizacionAsync(string version)
        {
            // Antes esto tenia page=1 y pageSize=50 fijos, sin manera de pedir la
            // pagina siguiente: a partir de la cotizacion 51 el listado dejaba de
            // verlas en silencio, y con el lo hacian Reportes y el tablero de
            // Inicio. Se devuelven todas las vigentes; quien pagina es la pantalla.
            var cotizaciones = await QueryCotizacionCompleta()
                .AsNoTracking()
                .Where(c => c.FechaCancelacion == null)
                .OrderByDescending(c => c.FechaCotizacion)
                .ToListAsync();

            var response = cotizaciones
                .Select(MapToResponse)
                .ToList();

            return Ok(response);
        }

        public override async Task<IActionResult> GetCotizacionByIdAsync(string version, int idCotizacion)
        {
            var cotizacion = await QueryCotizacionCompleta()
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.CotizacionId == idCotizacion &&
                    c.FechaCancelacion == null
                );

            if (cotizacion == null)
                return NotFound();

            return Ok(MapToResponse(cotizacion));
        }

        public override async Task<IActionResult> CreateCotizacionAsync(string version, [FromBody] CotizacionRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔥 VALIDACIÓN REAL
            if (body.CotizacionMercancia != null && body.CotizacionContenedor?.Any() == true)
                return BadRequest("Solo se permite un tipo de cotización");

            if (body.CotizacionMercancia == null && !(body.CotizacionContenedor?.Any() == true))
                return BadRequest("Debe enviar mercancía o contenedor");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cotizacion = new Cotizacion();

                MapToCotizacion(cotizacion, body);

                // 🔹 FORZAR fecha backend
                cotizacion.FechaCotizacion = DateTime.UtcNow;

                // 🔹 MERCANCIA
                if (body.CotizacionMercancia != null)
                {
                    var mercancia = new CotizacionMercancia();

                    MapToCotizacionMercancia(mercancia, body.CotizacionMercancia);

                    cotizacion.CotizacionMercancia = mercancia;
                }

                // 🔹 CONTENEDOR
                if (body.CotizacionContenedor != null && body.CotizacionContenedor.Any())
                {
                    cotizacion.CotizacionContenedor = new List<CotizacionContenedor>();

                    foreach (var item in body.CotizacionContenedor)
                    {
                        var contenedor = new CotizacionContenedor();

                        MapToCotizacionContenedor(contenedor, item);

                        cotizacion.CotizacionContenedor.Add(contenedor);
                    }
                }

                // 🔥 CÁLCULOS CENTRALIZADOS
                CalcularImportes(cotizacion);

                if (body.BienCotizacion?.Any() == true)
                {
                    cotizacion.BienCotizacion = new List<BienCotizacion>();

                    foreach (var item in body.BienCotizacion)
                    {
                        var bien = new BienCotizacion();

                        MapToBien(bien, item);

                        cotizacion.BienCotizacion.Add(bien);
                    }
                }

                if (body.CoberturaCotizacion?.Any() == true)
                {
                    cotizacion.CoberturaCotizacion = new List<CoberturaCotizacion>();

                    foreach (var item in body.CoberturaCotizacion)
                    {
                        var cobertura = new CoberturaCotizacion();

                        MapToCobertura(cobertura, item);

                        cotizacion.CoberturaCotizacion.Add(cobertura);
                    }
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

            // 🔥 VALIDACIÓN CLAVE
            if (body.CotizacionMercancia != null && body.CotizacionContenedor?.Any() == true)
                return BadRequest("Solo se permite un tipo de cotización");

            if (body.CotizacionMercancia == null && !(body.CotizacionContenedor?.Any() == true))
                return BadRequest("Debe enviar mercancía o contenedor");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cotizacion = await _context.Cotizacion
                    .Include(c => c.CotizacionContenedor)
                    .Include(c => c.CotizacionMercancia)
                    .Include(c => c.BienCotizacion)
                    .Include(c => c.CoberturaCotizacion)
                    .FirstOrDefaultAsync(c => c.CotizacionId == idCotizacion);

                if (cotizacion == null)
                    return NotFound();

                if (cotizacion.FechaCancelacion != null)
                    return BadRequest("La cotización está cancelada y no puede modificarse");

                // 🔹 actualizar datos generales (excepto fecha)
                var fechaOriginal = cotizacion.FechaCotizacion;

                MapToCotizacion(cotizacion, body);

                cotizacion.FechaCotizacion = fechaOriginal; // 🔒 proteger fecha

                // 🔹 CONTENEDOR
                if (body.CotizacionContenedor != null && body.CotizacionContenedor.Any())
                {

                    if (cotizacion.CotizacionMercancia != null)
                    {
                        _context.CotizacionMercancia.Remove(cotizacion.CotizacionMercancia);
                        cotizacion.CotizacionMercancia = null;
                    }

                    // eliminar anteriores
                    if (cotizacion.CotizacionContenedor != null)
                    {
                        _context.CotizacionContenedor.RemoveRange(cotizacion.CotizacionContenedor);
                    }

                    cotizacion.CotizacionContenedor = new List<CotizacionContenedor>();

                    foreach (var item in body.CotizacionContenedor)
                    {
                        var contenedor = new CotizacionContenedor
                        {
                            CotizacionId = cotizacion.CotizacionId
                        };

                        MapToCotizacionContenedor(contenedor, item);

                        cotizacion.CotizacionContenedor.Add(contenedor);
                    }
                }

                // 🔹 MERCANCIA
                if (body.CotizacionMercancia != null)
                {
                    if (cotizacion.CotizacionContenedor != null)
                    {
                        _context.CotizacionContenedor.RemoveRange(cotizacion.CotizacionContenedor);
                        cotizacion.CotizacionContenedor = null;
                    }

                    if (cotizacion.CotizacionMercancia == null)
                    {
                        cotizacion.CotizacionMercancia = new CotizacionMercancia
                        {
                            CotizacionId = cotizacion.CotizacionId
                        };

                        _context.CotizacionMercancia.Add(cotizacion.CotizacionMercancia);
                    }

                    MapToCotizacionMercancia(
                        cotizacion.CotizacionMercancia,
                        body.CotizacionMercancia
                    );
                }

                // 🔥 RECALCULAR SIEMPRE
                CalcularImportes(cotizacion);

                if (body.BienCotizacion != null)
                {
                    if (cotizacion.BienCotizacion != null)
                        _context.BienCotizacion.RemoveRange(cotizacion.BienCotizacion);

                    cotizacion.BienCotizacion = null;

                    if (body.BienCotizacion.Any())
                    {
                        cotizacion.BienCotizacion = new List<BienCotizacion>();

                        foreach (var item in body.BienCotizacion)
                        {
                            var bien = new BienCotizacion
                            {
                                CotizacionId = cotizacion.CotizacionId
                            };

                            MapToBien(bien, item);

                            cotizacion.BienCotizacion.Add(bien);
                        }
                    }
                }

                // Igual que los bienes: null deja las que ya tenia, lista vacia las
                // borra, y con elementos se reemplazan por completo.
                if (body.CoberturaCotizacion != null)
                {
                    if (cotizacion.CoberturaCotizacion != null)
                        _context.CoberturaCotizacion.RemoveRange(cotizacion.CoberturaCotizacion);

                    cotizacion.CoberturaCotizacion = null;

                    if (body.CoberturaCotizacion.Any())
                    {
                        cotizacion.CoberturaCotizacion = new List<CoberturaCotizacion>();

                        foreach (var item in body.CoberturaCotizacion)
                        {
                            var cobertura = new CoberturaCotizacion
                            {
                                CotizacionId = cotizacion.CotizacionId
                            };

                            MapToCobertura(cobertura, item);

                            cotizacion.CoberturaCotizacion.Add(cobertura);
                        }
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

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                cotizacion.FechaCancelacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Cotización cancelada correctamente",
                    cotizacionId = cotizacion.CotizacionId
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Error al cancelar la cotización",
                    detail = ex.Message
                });
            }
        }
    }
}
