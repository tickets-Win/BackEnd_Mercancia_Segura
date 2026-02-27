using System;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class BeneficiarioPreferenteApiController : Controllers.BeneficiarioPreferenteApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public BeneficiarioPreferenteApiController(ServiceDbContext context)
        {
            _context = context;
        }

        // MAP ENTITY → RESPONSE
        private BeneficiarioPreferenteResponse MapToResponse(BeneficiarioPreferente b)
        {
            return new BeneficiarioPreferenteResponse
            {
                BeneficiarioPreferenteId = b.BeneficiarioPreferenteId,
                Nombre = b.Nombre,
                ApellidoPaterno = b.ApellidoPaterno,
                ApellidoMaterno = b.ApellidoMaterno,
                RFC = b.RFC,
                Clave = b.Clave,
                TipoPersonaId = b.TipoPersonaId,
                tipo = b.TipoPersona != null ? b.TipoPersona.Tipo : null,
                RfcGenericoId = b.RfcGenericoId,
                rfcGenerico = b.RfcGenerico != null ? b.RfcGenerico.Tipo : null,
                Domicilio = b.Domicilio,
                NombreCompleto = b.NombreCompleto,
                Calle = b.Calle,
                NumeroInt = b.NumeroInt,
                NumeroExt = b.NumeroExt,
                Poblacion = b.Poblacion,
                Colonia = b.Colonia,
                Cp = b.Cp,
                Pais = b.Pais,
                Nacionalidad = b.Nacionalidad,
                FechaRegistro = b.FechaRegistro,
                FechaActualizacion = b.FechaActualizacion
            };
        }

        // MAP REQUEST → ENTITY
        private void MapToEntity(BeneficiarioPreferente entity, BeneficiarioPreferenteRequest body)
        {
            entity.Nombre = body.Nombre;
            entity.ApellidoPaterno = body.ApellidoPaterno;
            entity.ApellidoMaterno = body.ApellidoMaterno;
            entity.RFC = body.RFC;
            entity.Clave = body.Clave;
            entity.TipoPersonaId = body.TipoPersonaId;
            entity.RfcGenericoId = body.RfcGenericoId;
            entity.Calle = body.Calle;
            entity.NumeroInt = body.NumeroInt;
            entity.NumeroExt = body.NumeroExt;
            entity.Poblacion = body.Poblacion;
            entity.Colonia = body.Colonia;
            entity.Cp = body.Cp;
            entity.Pais = body.Pais;
            entity.Nacionalidad = body.Nacionalidad;
            entity.Domicilio = body.Domicilio;
            entity.NombreCompleto = body.NombreCompleto;
        }

        // GET ALL
        public override async Task<IActionResult> GetBeneficiarioPreferenteAsync(string version)
        {
            var lista = await _context.BeneficiarioPreferente
                .AsNoTracking()
                .Include(b => b.TipoPersona)
                .Include(b => b.RfcGenerico)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();

            return Ok(lista.Select(MapToResponse));
        }

        // GET BY ID
        public override async Task<IActionResult> GetBeneficiarioPreferenteByIdAsync(string version, int id)
        {
            var entity = await _context.BeneficiarioPreferente
                .Include(b => b.TipoPersona)
                .Include(b => b.RfcGenerico)
                .FirstOrDefaultAsync(b => b.BeneficiarioPreferenteId == id);

            if (entity == null)
                return NotFound();

            return Ok(MapToResponse(entity));
        }

        // CREATE
        public override async Task<IActionResult> CreateBeneficiarioPreferenteAsync(string version, [FromBody] BeneficiarioPreferenteRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new BeneficiarioPreferente
            {
                FechaRegistro = DateTime.Now
            };

            MapToEntity(entity, body);

            _context.BeneficiarioPreferente.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(MapToResponse(entity));
        }

        // UPDATE
        public override async Task<IActionResult> UpdateBeneficiarioPreferenteAsync(string version, int id, [FromBody] BeneficiarioPreferenteRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _context.BeneficiarioPreferente
                .FirstOrDefaultAsync(b => b.BeneficiarioPreferenteId == id);

            if (entity == null)
                return NotFound();

            MapToEntity(entity, body);
            entity.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(entity));
        }

        // DELETE (Hard Delete porque no tienes FechaBaja)
        public override async Task<IActionResult> DeleteBeneficiarioPreferenteAsync(string version, int id)
        {
            var entity = await _context.BeneficiarioPreferente
                .FirstOrDefaultAsync(b => b.BeneficiarioPreferenteId == id);

            if (entity == null)
                return NotFound();

            _context.BeneficiarioPreferente.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Beneficiario eliminado correctamente" });
        }
    }
}