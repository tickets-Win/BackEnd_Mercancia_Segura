using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class PolizaApiController : Controllers.Catalogos.PolizaApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public PolizaApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private PolizaResponse MapToResponse(Poliza p)
        {
            return new PolizaResponse
            {
                PolizaId = p.PolizaId,

                ProductoId = p.ProductoId,
                NombreProducto = p.Producto?.Nombre,

                ContratanteId = p.ContratanteId,
                NombreContratante = p.Contratante?.Nombre,

                AseguradoraId = p.AseguradoraId,
                NombreAseguradora = p.Aseguradora?.Nombre,

                SubRamoId = p.SubRamoId,
                NombreSubRamo = p.SubRamo?.Nombre,

                MonedaId = p.MonedaId,
                NombreMoneda = p.Moneda?.Nombre,

                FormaPagoId = p.FormaPagoId,
                NombreFormaPago = p.FormaPago?.Nombre,

                EstatusPolizaId = p.EstatusPolizaId,
                NombreEstatusPoliza = p.EstatusPoliza?.Tipo,

                NumeroPoliza = p.NumeroPoliza,
                ClaveAgente = p.ClaveAgente,
                FolioPoliza = p.FolioPoliza,
                TipoPoliza = p.TipoPoliza,

                VigenciaDel = p.VigenciaDel,
                VigenciaHasta = p.VigenciaHasta,

                FechaRegistro = p.FechaRegistro,
                FechaActualizacion = p.FechaActualizacion,
                FechaBaja = p.FechaBaja,

                PolizaContenedor = p.PolizaContenedor == null ? null : new PolizaContenedorResponse
                {
                    PolizaContenedorId = p.PolizaContenedor.PolizaContenedorId,
                    PolizaId = p.PolizaContenedor.PolizaId,
                    NombreInternoPoliza = p.PolizaContenedor.NombreInternoPoliza,
                    ManiobrasRescate = p.PolizaContenedor.ManiobrasRescate,

                    PorContenedor = p.PolizaContenedor.PorContenedor,
                    Ferrocarril = p.PolizaContenedor.Ferrocarril,
                    Terrestre = p.PolizaContenedor.Terrestre,
                    CuotaAplicable = p.PolizaContenedor.CuotaAplicable,
                    DanioMaterial = p.PolizaContenedor.DanioMaterial,
                    Robo = p.PolizaContenedor.Robo,
                    PerdidaParcial = p.PolizaContenedor.PerdidaParcial,
                    PerdidaTotal = p.PolizaContenedor.PerdidaTotal,
                    TrayectosAsegurados = p.PolizaContenedor.TrayectosAsegurados,
                    MedioTransporte = p.PolizaContenedor.MedioTransporte,
                    PrimaNeta = p.PolizaContenedor.PrimaNeta,
                    DerechoPoliza = p.PolizaContenedor.DerechoPoliza,
                    OtroPrima = p.PolizaContenedor.OtroPrima,
                    IVA = p.PolizaContenedor.IVA,
                    PrimaTotal = p.PolizaContenedor.PrimaTotal,

                    Cobertura = p.PolizaContenedor.Cobertura?
    .Select(c => new CoberturaResponse
    {
        CoberturaId = c.CoberturaId,
        PolizaContenedorId = c.PolizaContenedorId,
        Nombre = c.Nombre
    }).ToList() ?? new List<CoberturaResponse>()

                },
                PolizaMercancia = p.PolizaMercancia?
    .Select(pm => new PolizaMercanciaResponse
    {
        PolizaMercanciaId = pm.PolizaMercanciaId,
        PolizaId = pm.PolizaId,
        AdministracionBienId = pm.AdministracionBienId,
        NombreInternoPoliza = pm.NombreInternoPoliza,

        TerrestreAereo = pm.TerrestreAereo,
        Maritimo = pm.Maritimo,
        PaqueteriaMensajeria = pm.PaqueteriaMensajeria,

        Deducibles = pm.Deducibles,
        Compras = pm.Compras,
        Ventas = pm.Ventas,
        Maquila = pm.Maquila,
        BienesUsados = pm.BienesUsados,
        EmbarqueFiliales = pm.EmbarqueFiliales,
        IndemnizacionOtros = pm.IndemnizacionOtros,

        Medicamentos = pm.Medicamentos,
        CobreAluminioAcero = pm.CobreAluminioAcero,
        MedicamentosControlados = pm.MedicamentosControlados,
        EqContratistas = pm.EqContratistas,
        CuotaGeneralPoliza = pm.CuotaGeneralPoliza,

        PrimaNeta = pm.PrimaNeta,
        DerechoPoliza = pm.DerechoPoliza,
        OtroPrima = pm.OtroPrima,
        IVA = pm.IVA,
        PrimaTotal = pm.PrimaTotal,

        RiesgoCubierto = pm.RiesgoCubierto?
    .Select(r => new RiesgoCubiertoResponse
    {
        RiesgoCubiertoId = r.RiesgoCubiertoId,
        Nombre = r.Nombre,
        TipoRiesgoId = r.TipoRiesgoId,
        PolizaMercanciaId = r.PolizaMercanciaId,
        AdministracionBienId = r.AdministracionBienId,
    }).ToList() ?? new List<RiesgoCubiertoResponse>()

    }).ToList(),
                Bien = p.Bien?
    .Select(b => new BienResponse
    {
        BienId = b.BienId,
        PolizaId = b.PolizaId,
        TipoBienId = b.TipoBienId,
        AdministracionBienId = b.AdministracionBienId,
        NombreTipoBien = b.TipoBien?.Nombre,
        Nombre = b.Nombre
    }).ToList() ?? new List<BienResponse>()

            };
        }


        private void MapToPoliza(Poliza poliza, PolizaRequest body)
        {
            if (poliza == null || body == null) return;

            // 📌 Relaciones / Catálogos
            poliza.ProductoId = body.ProductoId;
            poliza.ContratanteId = body.ContratanteId;
            poliza.AseguradoraId = body.AseguradoraId;
            poliza.SubRamoId = body.SubRamoId;
            poliza.MonedaId = body.MonedaId;
            poliza.FormaPagoId = body.FormaPagoId;
            poliza.EstatusPolizaId = body.EstatusPolizaId;

            // 📌 Datos generales
            poliza.NumeroPoliza = body.NumeroPoliza;
            poliza.ClaveAgente = body.ClaveAgente;
            poliza.FolioPoliza = body.FolioPoliza;
            poliza.TipoPoliza = body.TipoPoliza;

            // 📌 Vigencia
            poliza.VigenciaDel = body.VigenciaDel;
            poliza.VigenciaHasta = body.VigenciaHasta;

        }

        private void MapToPolizaContenedor(PolizaContenedor pc, PolizaContenedorRequest body)
        {
            if (pc == null || body == null) return;

            // 📌 Datos generales
            pc.NombreInternoPoliza = body.NombreInternoPoliza;
            pc.PorContenedor = body.PorContenedor;
            pc.Ferrocarril = body.Ferrocarril;
            pc.Terrestre = body.Terrestre;

            // 📌 Coberturas
            pc.DanioMaterial = body.DanioMaterial;
            pc.Robo = body.Robo;
            pc.PerdidaParcial = body.PerdidaParcial;
            pc.PerdidaTotal = body.PerdidaTotal;

            // 📌 Configuración
            pc.CuotaAplicable = body.CuotaAplicable;
            pc.TrayectosAsegurados = body.TrayectosAsegurados;
            pc.MedioTransporte = body.MedioTransporte;
            pc.ManiobrasRescate = body.ManiobrasRescate;

            pc.PrimaNeta = body.PrimaNeta;
            pc.DerechoPoliza = body.DerechoPoliza;
            pc.OtroPrima = body.OtroPrima;
            pc.IVA = body.IVA;
            pc.PrimaTotal = body.PrimaTotal;
        }

        private void MapToPolizaMercancia(PolizaMercancia pm, PolizaMercanciaRequest body)
        {
            if (pm == null || body == null) return;

            // 📌 Datos generales
            pm.NombreInternoPoliza = body.NombreInternoPoliza;
            pm.AdministracionBienId = body.AdministracionBienId;

            // 📌 Medios de transporte
            pm.TerrestreAereo = body.TerrestreAereo;
            pm.Maritimo = body.Maritimo;
            pm.PaqueteriaMensajeria = body.PaqueteriaMensajeria;

            // 📌 Operaciones
            pm.Compras = body.Compras;
            pm.Ventas = body.Ventas;
            pm.Maquila = body.Maquila;
            pm.EmbarqueFiliales = body.EmbarqueFiliales;
            pm.IndemnizacionOtros = body.IndemnizacionOtros;

            // 📌 Bienes especiales
            pm.BienesUsados = body.BienesUsados;
            pm.Medicamentos = body.Medicamentos;
            pm.MedicamentosControlados = body.MedicamentosControlados;
            pm.CobreAluminioAcero = body.CobreAluminioAcero;
            pm.EqContratistas = body.EqContratistas;

            // 📌 Deducibles y cuota
            pm.Deducibles = body.Deducibles;
            pm.CuotaGeneralPoliza = body.CuotaGeneralPoliza;

            pm.PrimaNeta = body.PrimaNeta;
            pm.DerechoPoliza = body.DerechoPoliza;
            pm.OtroPrima = body.OtroPrima;
            pm.IVA = body.IVA;
            pm.PrimaTotal = body.PrimaTotal;
        }

        private void MapToBien(Bien bien, BienRequest body)
        {
            if (bien == null || body == null) return;

            // 📌 Tipo de bien
            bien.TipoBienId = body.TipoBienId;

            bien.AdministracionBienId = body?.AdministracionBienId;

            // 📌 Datos generales
            bien.Nombre = body.Nombre;
        }


        private IQueryable<Poliza> QueryPolizaCompleta()
        {
            return _context.Poliza
                .Include(p => p.Producto)
                .Include(p => p.Contratante)
                .Include(p => p.Aseguradora)
                .Include(p => p.SubRamo)
                .Include(p => p.Moneda)
                .Include(p => p.FormaPago)
                .Include(p => p.EstatusPoliza)
                .Include(p => p.PolizaContenedor)
                    .ThenInclude(pc => pc.Cobertura)
                .Include(p => p.PolizaMercancia)
                    .ThenInclude(pm => pm.RiesgoCubierto)
                .Include(p => p.Bien)
                    .ThenInclude(b => b.TipoBien);
        }




        public override async Task<IActionResult> GetPolizaAsync(string version)
        {
            var polizas = await QueryPolizaCompleta()
                .AsNoTracking()
                .Where(p => p.FechaBaja == null)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();

            var response = polizas
                .Select(p => MapToResponse(p))
                .ToList();

            return Ok(response);
        }

        public override async Task<IActionResult> GetPolizaByIdAsync(string version, int idPoliza)
        {
            var poliza = await QueryPolizaCompleta()
                .AsNoTracking()
                .Where(p => p.FechaBaja == null)
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            return Ok(MapToResponse(poliza));
        }
        public override async Task<IActionResult> CreatePolizaAsync(string version, [FromBody] PolizaRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Crear la póliza
                var poliza = new Poliza
                {
                    FechaRegistro = DateTime.Now,
                    EstatusPolizaId = body.EstatusPolizaId
                };
                MapToPoliza(poliza, body);

                // 🔹 PolizaContenedor o PolizaMercancia
                if (body.PolizaContenedor != null)
                {
                    // Solo creamos PolizaContenedor si existe
                    var contenedor = new PolizaContenedor();
                    MapToPolizaContenedor(contenedor, body.PolizaContenedor);
                    poliza.PolizaContenedor = contenedor;

                    contenedor.Cobertura = body.PolizaContenedor.Cobertura?
                        .Select(c => new Cobertura
                        {
                            Nombre = c.Nombre
                        }).ToList() ?? new();
                }
                else if (body.PolizaMercancia != null && body.PolizaMercancia.Any())
                {
                    // Solo creamos PolizaMercancia si no hay contenedor
                    poliza.PolizaMercancia = new List<PolizaMercancia>();
                    foreach (var item in body.PolizaMercancia)
                    {
                        var mercancia = new PolizaMercancia();

                        MapToPolizaMercancia(mercancia, item);

                        poliza.PolizaMercancia.Add(mercancia);
                    }
                }


                if (body.Bien?.Any() == true)
                {
                    poliza.Bien = new List<Bien>();
                    foreach (var b in body.Bien)
                    {
                        var bien = new Bien();
                        MapToBien(bien, b);
                        poliza.Bien.Add(bien);
                    }
                }

                // Insertar todo en la BD
                _context.Poliza.Add(poliza);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var polizaCreada = await QueryPolizaCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PolizaId == poliza.PolizaId);

                return Ok(MapToResponse(polizaCreada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public override async Task<IActionResult> UpdatePolizaAsync(string version, int idPoliza, [FromBody] PolizaRequest body)
        {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            var poliza = await _context.Poliza
                .Include(p => p.PolizaContenedor)
                    .ThenInclude(pc => pc.Cobertura)

                .Include(p => p.PolizaMercancia)
                    .ThenInclude(pm => pm.RiesgoCubierto)

                .Include(p => p.Bien)

                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

                if (poliza == null)
                    return NotFound();

                if (poliza.FechaBaja != null)
                    return BadRequest("La póliza está cancelada y no puede modificarse");

                // 🔹 Actualizar datos generales
                MapToPoliza(poliza, body);
            poliza.FechaActualizacion = DateTime.Now;

            if (body.PolizaContenedor != null)
            {
                if (poliza.PolizaContenedor == null)
                {
                    poliza.PolizaContenedor = new PolizaContenedor
                    {
                        PolizaId = poliza.PolizaId
                    };
                    _context.PolizaContenedor.Add(poliza.PolizaContenedor);
                }

                MapToPolizaContenedor(poliza.PolizaContenedor, body.PolizaContenedor);

                    if (poliza.PolizaContenedor?.Cobertura?.Any() == true)
                    {
                        _context.Cobertura.RemoveRange(poliza.PolizaContenedor.Cobertura);
                    }

                    poliza.PolizaContenedor.Cobertura = body.PolizaContenedor.Cobertura?
                        .Select(c => new Cobertura
                        {
                            Nombre = c.Nombre
                        }).ToList() ?? new List<Cobertura>();
                }

            if (body.PolizaMercancia != null && body.PolizaMercancia.Any())
            {
                // eliminar anteriores
                if (poliza.PolizaMercancia != null)
                {
                    foreach (var pm in poliza.PolizaMercancia)
                    {
                        if (pm.RiesgoCubierto != null)
                            _context.RiesgoCubierto.RemoveRange(pm.RiesgoCubierto);
                    }

                    _context.PolizaMercancia.RemoveRange(poliza.PolizaMercancia);
                }

                poliza.PolizaMercancia = new List<PolizaMercancia>();

                foreach (var item in body.PolizaMercancia)
                {
                    var mercancia = new PolizaMercancia();

                    MapToPolizaMercancia(mercancia, item);

                    poliza.PolizaMercancia.Add(mercancia);
                }
            }

            if (body.Bien != null)
            {
                if (poliza.Bien != null)
                    _context.Bien.RemoveRange(poliza.Bien);

                poliza.Bien = new List<Bien>();

                foreach (var b in body.Bien)
                {
                    var bien = new Bien();
                    MapToBien(bien, b);
                    poliza.Bien.Add(bien);
                }
            }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var polizaActualizada = await QueryPolizaCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

                return Ok(MapToResponse(polizaActualizada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task<IActionResult> DeletePolizaAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound(new { message = "La póliza no existe" });

            if (poliza.FechaBaja != null)
                return BadRequest(new { message = "La póliza ya está cancelada" });

            try
            {
                poliza.EstatusPolizaId = 3; // 3 = Cancelada
                poliza.FechaBaja = DateTime.Now;
                poliza.FechaActualizacion = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Póliza cancelada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cancelar la póliza", detail = ex.Message });
            }
        }

    }
}
