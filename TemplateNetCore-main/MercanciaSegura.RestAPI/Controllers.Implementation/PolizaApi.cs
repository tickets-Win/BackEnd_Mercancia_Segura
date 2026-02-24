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
        PolizaMercanciaId = r.PolizaMercanciaId
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

            // 📌 Lista de Coberturas
            pc.Cobertura = body.Cobertura?
                .Select(c => new Cobertura
                {
                    Nombre = c.Nombre
                })
                .ToList() ?? new List<Cobertura>();
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

            // 📌 Riesgos cubiertos
            pm.RiesgoCubierto = body.RiesgoCubierto?
                .Select(r => new RiesgoCubierto
                {
                    Nombre = r.Nombre,
                    TipoRiesgoId = r.TipoRiesgoId
                })
                .ToList() ?? new List<RiesgoCubierto>();
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




        public override async Task<IActionResult> GetPolizaAsync(string version)
        {
            // Obtener todas las pólizas activas
            var polizas = await _context.Poliza
                .AsNoTracking()
                .Where(p => p.FechaBaja == null) // Soft delete
                                                 // Catálogos relacionados
                .Include(p => p.Producto)
                .Include(p => p.Contratante)
                .Include(p => p.Aseguradora)
                .Include(p => p.SubRamo)
                .Include(p => p.Moneda)
                .Include(p => p.FormaPago)
                .Include(p => p.EstatusPoliza)
                // PolizaContenedor y sus coberturas
                .Include(p => p.PolizaContenedor)
                    .ThenInclude(pc => pc.Cobertura)
                // PolizaMercancia y sus riesgos
                .Include(p => p.PolizaMercancia)
                    .ThenInclude(pm => pm.RiesgoCubierto)
                // Bienes y tipo de bien
                .Include(p => p.Bien)
                    .ThenInclude(b => b.TipoBien)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();

            // Mapear a response
            var response = polizas
                .Select(p => MapToResponse(p))
                .ToList();

            // Retornar
            return Ok(response);
        }

        public override async Task<IActionResult> GetPolizaByIdAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .AsNoTracking()
                .Where(p => p.FechaBaja == null) // Respeta soft delete
                .Include(p => p.Producto)
                .Include(p => p.Contratante)
                .Include(p => p.Aseguradora)
                .Include(p => p.SubRamo)
                .Include(p => p.Moneda)
                .Include(p => p.FormaPago)
                .Include(p => p.EstatusPoliza)
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
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
                }
                else if (body.PolizaMercancia != null && body.PolizaMercancia.Any())
                {
                    // Solo creamos PolizaMercancia si no hay contenedor
                    poliza.PolizaMercancia = new List<PolizaMercancia>();
                    foreach (var item in body.PolizaMercancia)
                    {
                        var mercancia = new PolizaMercancia
                        {
                            AdministracionBienId = item.AdministracionBienId
                        };
                        MapToPolizaMercancia(mercancia, item);

                        if (item.RiesgoCubierto != null && item.RiesgoCubierto.Any())
                        {
                            mercancia.RiesgoCubierto = item.RiesgoCubierto
                                .Select(r => new RiesgoCubierto
                                {
                                    Nombre = r.Nombre,
                                    TipoRiesgoId = r.TipoRiesgoId
                                })
                                .ToList();
                        }

                        poliza.PolizaMercancia.Add(mercancia);
                    }
                }


                // 🔹 Bienes
                if (body.Bien != null && body.Bien.Any())
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

                // Cargar para respuesta
                var polizaCreada = await _context.Poliza
                    .AsNoTracking()
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
                        .ThenInclude(b => b.TipoBien)
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

            var poliza = await _context.Poliza
                .Include(p => p.Producto)
                .Include(p => p.Contratante)
                .Include(p => p.Aseguradora)
                .Include(p => p.SubRamo)
                .Include(p => p.Moneda)
                .Include(p => p.FormaPago)
                .Include(p => p.EstatusPoliza)
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            // 🔹 Actualizar datos de la póliza
            MapToPoliza(poliza, body);
            poliza.FechaActualizacion = DateTime.Now;

            // 🔹 Actualizar o crear PolizaContenedor
            if (body.PolizaContenedor != null)
            {
                if (poliza.PolizaContenedor == null)
                {
                    poliza.PolizaContenedor = new PolizaContenedor { PolizaId = poliza.PolizaId };
                    _context.PolizaContenedor.Add(poliza.PolizaContenedor);
                }
                MapToPolizaContenedor(poliza.PolizaContenedor, body.PolizaContenedor);
            }


            await _context.SaveChangesAsync();

            return Ok(MapToResponse(poliza));
        }

        public override async Task<IActionResult> DeletePolizaAsync(string version, int idPoliza)
        {
            // Buscar la póliza
            var poliza = await _context.Poliza
                .Include(p => p.PolizaContenedor)
                    .ThenInclude(pc => pc.Cobertura)
                .Include(p => p.PolizaMercancia)
                    .ThenInclude(pm => pm.RiesgoCubierto)
                .Include(p => p.Bien)
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound(new { message = "La póliza no existe" });

            if (poliza.EstatusPolizaId == 3)
                return BadRequest(new { message = "La póliza ya está cancelada" });

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Marcar como cancelada
                poliza.EstatusPolizaId = 3;
                poliza.FechaActualizacion = DateTime.Now;

                // Opcional: marcar hijos como inactivos o limpiar datos
                if (poliza.PolizaContenedor != null)
                {
                    poliza.PolizaContenedor.TrayectosAsegurados = null;
                    poliza.PolizaContenedor.Cobertura?.Clear();
                }

                if (poliza.PolizaMercancia != null)
                {
                    foreach (var pm in poliza.PolizaMercancia)
                    {
                        pm.RiesgoCubierto?.Clear();
                    }
                }

                if (poliza.Bien != null)
                {
                    poliza.Bien.Clear();
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Póliza cancelada correctamente" });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
