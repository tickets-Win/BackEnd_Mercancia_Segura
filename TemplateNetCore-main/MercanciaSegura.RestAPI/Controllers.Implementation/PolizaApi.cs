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

                VigenciaDel = p.VigenciaDel,
                VigenciaHasta = p.VigenciaHasta,

                OtrosPoliza = p.OtrosPoliza,

                PrimaNeta = p.PrimaNeta,
                DerechoPoliza = p.DerechoPoliza,
                Otros = p.Otros,
                IVA = p.IVA,
                PrimaTotal = p.PrimaTotal,

                FechaRegistro = p.FechaRegistro,
                FechaActualizacion = p.FechaActualizacion,
                FechaBaja = p.FechaBaja,

                PolizaContenedor = p.PolizaContenedor == null ? null : new PolizaContenedorResponse
                {
                    BienesAsegurados = p.PolizaContenedor.BienesAsegurados,
                    PorContenedor = p.PolizaContenedor.PorContenedor,
                    Ferrocarril = p.PolizaContenedor.Ferrocarril,
                    Terrestre = p.PolizaContenedor.Terrestre,
                    CuotaAplicable = p.PolizaContenedor.CuotaAplicable,
                    DanioMaterial = p.PolizaContenedor.DanioMaterial,
                    Robo = p.PolizaContenedor.Robo,
                    PerdidaParcial = p.PolizaContenedor.PerdidaParcial,
                    PerdidaTotal = p.PolizaContenedor.PerdidaTotal,
                    TrayectosAsegurados = p.PolizaContenedor.TrayectosAsegurados,
                    MedioTransporte = p.PolizaContenedor.MedioTransporte
                },

                PolizaMercancia = p.PolizaMercancia == null ? null : new PolizaMercanciaResponse
                {
                    NombreInternoPoliza = p.PolizaMercancia.NombreInternoPoliza,
                    FolioPoliza = p.PolizaMercancia.FolioPoliza,
                    BienesAsegurados = p.PolizaMercancia.BienesAsegurados,
                    BienesExcluidos = p.PolizaMercancia.BienesExcluidos,
                    BienesSujetosAConsulta = p.PolizaMercancia.BienesSujetosAConsulta,
                    TerrestreAereo = p.PolizaMercancia.TerrestreAereo,
                    Maritimo = p.PolizaMercancia.Maritimo,
                    PaqueteriaMensajeria = p.PolizaMercancia.PaqueteriaMensajeria,
                    Deducibles = p.PolizaMercancia.Deducibles,
                    Compras = p.PolizaMercancia.Compras,
                    Ventas = p.PolizaMercancia.Ventas,
                    Maquila = p.PolizaMercancia.Maquila,
                    BienesUsados = p.PolizaMercancia.BienesUsados,
                    EmbarqueFiliales = p.PolizaMercancia.EmbarqueFiliales,
                    IndemnizacionOtros = p.PolizaMercancia.IndemnizacionOtros,
                    Medicamentos = p.PolizaMercancia.Medicamentos,
                    CobreAluminioAcero = p.PolizaMercancia.CobreAluminioAcero,
                    MedicamentosControlados = p.PolizaMercancia.MedicamentosControlados,
                    EqContratistas = p.PolizaMercancia.EqContratistas,
                    CuotaGeneralPoliza = p.PolizaMercancia.CuotaGeneralPoliza
                },

                PolizaCobertura = p.PolizaCobertura == null ? null : new PolizaCoberturaResponse
                {
                    Deducible = p.PolizaCobertura.Deducible,
                    Prima = p.PolizaCobertura.Prima,
                    SumaAsegurada = p.PolizaCobertura.SumaAsegurada,

                    Cobertura = p.PolizaCobertura.Cobertura == null
                        ? null
                        : p.PolizaCobertura.Cobertura.Select(c => new CoberturaResponse
                        {
                            RiesgoId = c.RiesgoId,
                            Nombre = c.Nombre
                        }).ToList()
                }
            };
        }


        private void MapToPoliza(Poliza poliza, PolizaRequest body)
        {
            poliza.ProductoId = body.ProductoId;
            poliza.ContratanteId = body.ContratanteId;
            poliza.AseguradoraId = body.AseguradoraId;
            poliza.SubRamoId = body.SubRamoId;
            poliza.MonedaId = body.MonedaId;
            poliza.FormaPagoId = body.FormaPagoId;
            poliza.EstatusPolizaId = body.EstatusPolizaId;

            poliza.NumeroPoliza = body.NumeroPoliza;
            poliza.ClaveAgente = body.ClaveAgente;
            poliza.FolioPoliza = body.FolioPoliza;

            poliza.VigenciaDel = body.VigenciaDel;
            poliza.VigenciaHasta = body.VigenciaHasta;

            poliza.OtrosPoliza = body.OtrosPoliza;

            poliza.PrimaNeta = body.PrimaNeta;
            poliza.DerechoPoliza = body.DerechoPoliza;
            poliza.Otros = body.Otros;
            poliza.IVA = body.IVA;
            poliza.PrimaTotal = body.PrimaTotal;
        }

        private void MapToPolizaContenedor(PolizaContenedor pc, PolizaContenedorRequest body)
        {
            pc.BienesAsegurados = body.BienesAsegurados;
            pc.PorContenedor = body.PorContenedor;
            pc.Ferrocarril = body.Ferrocarril;
            pc.Terrestre = body.Terrestre;
            pc.CuotaAplicable = body.CuotaAplicable;
            pc.DanioMaterial = body.DanioMaterial;
            pc.Robo = body.Robo;
            pc.PerdidaParcial = body.PerdidaParcial;
            pc.PerdidaTotal = body.PerdidaTotal;
            pc.TrayectosAsegurados = body.TrayectosAsegurados;
            pc.MedioTransporte = body.MedioTransporte;
        }
        private void MapToPolizaMercancia(PolizaMercancia pm, PolizaMercanciaRequest body)
        {
            pm.NombreInternoPoliza = body.NombreInternoPoliza;
            pm.FolioPoliza = body.FolioPoliza;
            pm.BienesAsegurados = body.BienesAsegurados;
            pm.BienesExcluidos = body.BienesExcluidos;
            pm.BienesSujetosAConsulta = body.BienesSujetosAConsulta;

            pm.TerrestreAereo = body.TerrestreAereo;
            pm.Maritimo = body.Maritimo;
            pm.PaqueteriaMensajeria = body.PaqueteriaMensajeria;

            pm.Deducibles = body.Deducibles;
            pm.Compras = body.Compras;
            pm.Ventas = body.Ventas;
            pm.Maquila = body.Maquila;
            pm.BienesUsados = body.BienesUsados;
            pm.EmbarqueFiliales = body.EmbarqueFiliales;
            pm.IndemnizacionOtros = body.IndemnizacionOtros;

            pm.Medicamentos = body.Medicamentos;
            pm.CobreAluminioAcero = body.CobreAluminioAcero;
            pm.MedicamentosControlados = body.MedicamentosControlados;
            pm.EqContratistas = body.EqContratistas;

            pm.CuotaGeneralPoliza = body.CuotaGeneralPoliza;
        }

        private void MapToPolizaCobertura(PolizaCobertura poco, PolizaCoberturaRequest body)
        {
            poco.Deducible = body.Deducible;
            poco.Prima = body.Prima;
            poco.SumaAsegurada = body.SumaAsegurada;
        }

        private void MapToCobertura(Cobertura entity, CoberturaRequest body)
        {
            entity.RiesgoId = body.RiesgoId;
            entity.Nombre = body.Nombre;
        }

        public override async Task<IActionResult> GetPolizaAsync(string version)
{
    var polizas = await _context.Poliza
        .AsNoTracking()
        .Where(p => p.FechaBaja == null) // Soft delete
        .Include(p => p.Producto)
        .Include(p => p.Contratante)
        .Include(p => p.Aseguradora)
        .Include(p => p.SubRamo)
        .Include(p => p.Moneda)
        .Include(p => p.FormaPago)
        .Include(p => p.EstatusPoliza)
        .Include(p => p.PolizaContenedor)
        .Include(p => p.PolizaMercancia)
        .Include(p => p.PolizaCobertura)
            .ThenInclude(pc => pc.Cobertura)
        .OrderByDescending(p => p.FechaRegistro)
        .ToListAsync();

    var response = polizas.Select(MapToResponse);

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
                .Include(p => p.PolizaCobertura)
                    .ThenInclude(pc => pc.Cobertura)
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
                    FechaActualizacion = DateTime.Now,
                    EstatusPolizaId = body.EstatusPolizaId
                };
                MapToPoliza(poliza, body);

                // PolizaContenedor
                if (body.PolizaContenedor != null)
                {
                    var contenedor = new PolizaContenedor();
                    MapToPolizaContenedor(contenedor, body.PolizaContenedor);
                    poliza.PolizaContenedor = contenedor;
                }

                // PolizaMercancia
                if (body.PolizaMercancia != null)
                {
                    var mercancia = new PolizaMercancia();
                    MapToPolizaMercancia(mercancia, body.PolizaMercancia);
                    poliza.PolizaMercancia = mercancia;
                }

                // PolizaCobertura y Coberturas
                if (body.PolizaCobertura != null)
                {
                    var poco = new PolizaCobertura();
                    MapToPolizaCobertura(poco, body.PolizaCobertura);

                    if (body.PolizaCobertura.Cobertura != null && body.PolizaCobertura.Cobertura.Any())
                    {
                        poco.Cobertura = body.PolizaCobertura.Cobertura
                            .Select(c => new Cobertura
                            {
                                RiesgoId = c.RiesgoId,
                                Nombre = c.Nombre
                            }).ToList();
                    }

                    poliza.PolizaCobertura = poco;
                }

                // Insertar todo
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
                    .Include(p => p.PolizaMercancia)
                    .Include(p => p.PolizaCobertura)
                        .ThenInclude(pc => pc.Cobertura)
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
                .Include(p => p.PolizaCobertura)
                    .ThenInclude(pc => pc.Cobertura)
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

            // 🔹 Actualizar o crear PolizaMercancia
            if (body.PolizaMercancia != null)
            {
                if (poliza.PolizaMercancia == null)
                {
                    poliza.PolizaMercancia = new PolizaMercancia { PolizaId = poliza.PolizaId };
                    _context.PolizaMercancia.Add(poliza.PolizaMercancia);
                }
                MapToPolizaMercancia(poliza.PolizaMercancia, body.PolizaMercancia);
            }

            // 🔹 Actualizar o crear PolizaCobertura y sus Coberturas
            if (body.PolizaCobertura != null)
            {
                if (poliza.PolizaCobertura == null)
                {
                    poliza.PolizaCobertura = new PolizaCobertura { PolizaId = poliza.PolizaId };
                    _context.PolizaCobertura.Add(poliza.PolizaCobertura);
                }

                MapToPolizaCobertura(poliza.PolizaCobertura, body.PolizaCobertura);

                if (body.PolizaCobertura.Cobertura != null && body.PolizaCobertura.Cobertura.Any())
                {
                    // Eliminar las coberturas existentes si quieres reemplazar
                    _context.Cobertura.RemoveRange(poliza.PolizaCobertura.Cobertura ?? new List<Cobertura>());

                    foreach (var c in body.PolizaCobertura.Cobertura)
                    {
                        var cobertura = new Cobertura
                        {
                            PolizaCoberturaId = poliza.PolizaCobertura.PolizaCoberturaId,
                            RiesgoId = c.RiesgoId,
                            Nombre = c.Nombre
                        };
                        _context.Cobertura.Add(cobertura);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(poliza));
        }


        public override async Task<IActionResult> DeletePolizaAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            if (poliza.EstatusPolizaId == 3)
                return BadRequest(new { message = "La póliza ya está cancelada" });

            // Marcar como cancelada
            poliza.EstatusPolizaId = 3;
            poliza.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Póliza cancelada correctamente" });
        }


    }
}
