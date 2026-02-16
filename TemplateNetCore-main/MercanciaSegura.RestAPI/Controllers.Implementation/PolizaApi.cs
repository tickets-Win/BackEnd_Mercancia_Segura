using System;
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
                ContratanteId = p.ContratanteId,
                AseguradoraId = p.AseguradoraId,
                SubRamoId = p.SubRamoId,
                MonedaId = p.MonedaId,
                FormaPagoId = p.FormaPagoId,

                Estatus = p.Estatus,
                NumeroPoliza = p.NumeroPoliza,

                VigenciaDel = p.VigenciaDel,
                VigenciaHasta = p.VigenciaHasta,

                OtrosPoliza = p.OtrosPoliza,

                PrimaNeta = p.PrimaNeta,
                DerechoPoliza = p.DerechoPoliza,
                Otros = p.Otros,
                IVA = p.IVA,
                PrimaTotal = p.PrimaTotal,

                FechaRegistro = p.FechaRegistro,

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

                PolizaCobertura = p.PolizaCobertura?.Select(c => new PolizaCoberturaResponse
                {
                    Deducible = c.Deducible,
                    Prima = c.Prima,
                    SumaAsegurada = c.SumaAsegurada
                }).ToList()
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

            poliza.NumeroPoliza = body.NumeroPoliza;

            poliza.VigenciaDel = body.VigenciaDel;
            poliza.VigenciaHasta = body.VigenciaHasta;

            poliza.OtrosPoliza = body.OtrosPoliza;

            poliza.PrimaNeta = body.PrimaNeta;
            poliza.DerechoPoliza = body.DerechoPoliza;
            poliza.Otros = body.Otros;
            poliza.IVA = body.IVA;
            poliza.PrimaTotal = body.PrimaTotal;

            poliza.FechaActualizacion = DateTime.Now;
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


        public override async Task<IActionResult> GetPolizaAsync(string version)
        {
            var polizas = await _context.Poliza
                .AsNoTracking()
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
                .Include(p => p.PolizaCobertura)
                .ToListAsync();


            return Ok(polizas.Select(MapToResponse));
        }


        public override async Task<IActionResult> GetPolizaByIdAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .AsNoTracking()
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
                .Include(p => p.PolizaCobertura)
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            return Ok(MapToResponse(poliza));
        }


        public override async Task<IActionResult> CreatePolizaAsync(string version, [FromBody] PolizaRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var poliza = new Poliza
                {
                    FechaRegistro = DateTime.Now,
                    Estatus = true
                };

                MapToPoliza(poliza, body);

                _context.Poliza.Add(poliza);
                await _context.SaveChangesAsync();

                // 🔹 Crear PolizaContenedor si viene en el request
                if (body.PolizaContenedor != null)
                {
                    var contenedor = new PolizaContenedor
                    {
                        PolizaId = poliza.PolizaId
                    };

                    MapToPolizaContenedor(contenedor, body.PolizaContenedor);

                    _context.PolizaContenedor.Add(contenedor);
                }

                // 🔹 Crear PolizaMercancia si viene en el request
                if (body.PolizaMercancia != null)
                {
                    var mercancia = new PolizaMercancia
                    {
                        PolizaId = poliza.PolizaId
                    };

                    MapToPolizaMercancia(mercancia, body.PolizaMercancia);

                    _context.PolizaMercancia.Add(mercancia);
                }

                if (body.PolizaCobertura != null && body.PolizaCobertura.Any())
                {
                    foreach (var item in body.PolizaCobertura)
                    {
                        var cobertura = new PolizaCobertura
                        {
                            PolizaId = poliza.PolizaId,
                            Deducible = item.Deducible,
                            Prima = item.Prima,
                            SumaAsegurada = item.SumaAsegurada
                        };

                        _context.PolizaCobertura.Add(cobertura);
                    }

                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var polizaCreada = await _context.Poliza
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
                .Include(p => p.PolizaCobertura)
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
                .Include(p => p.PolizaContenedor)
                .Include(p => p.PolizaMercancia)
                .Include(p => p.PolizaCobertura)
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            MapToPoliza(poliza, body);

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(poliza));
        }

        public override async Task<IActionResult> DeletePolizaAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            if (poliza.Estatus == false)
                return BadRequest(new { message = "La póliza ya está desactivada" });

            poliza.Estatus = false;
            poliza.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Póliza desactivada correctamente" });
        }

    }
}
