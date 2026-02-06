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
                TipoPoliza = p.TipoPoliza,
                NumeroPoliza = p.NumeroPoliza,

                VigenciaDel = p.VigenciaDel,
                VigenciaHasta = p.VigenciaHasta,

                OtrosPoliza = p.OtrosPoliza,

                PrimaNeta = p.PrimaNeta,
                DerechoPoliza = p.DerechoPoliza,
                Otros = p.Otros,
                IVA = p.IVA,
                PrimaTotal = p.PrimaTotal,

                FechaRegistro = p.FechaRegistro
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

            poliza.TipoPoliza = body.TipoPoliza;
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
                .ToListAsync();

            return Ok(polizas.Select(MapToResponse));
        }


        public override async Task<IActionResult> GetPolizaByIdAsync(string version, int idPoliza)
        {
            var poliza = await _context.Poliza
                .FirstOrDefaultAsync(p => p.PolizaId == idPoliza);

            if (poliza == null)
                return NotFound();

            return Ok(MapToResponse(poliza));
        }


        public override async Task<IActionResult> CreatePolizaAsync(
    string version, [FromBody] PolizaRequest body)
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
                    await _context.SaveChangesAsync();
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
                    await _context.SaveChangesAsync();
                }


                await transaction.CommitAsync();

                return Ok(MapToResponse(poliza));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public override async Task<IActionResult> UpdatePolizaAsync(
    string version, int idPoliza, [FromBody] PolizaRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var poliza = await _context.Poliza
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
