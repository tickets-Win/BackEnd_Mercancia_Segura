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
    public class PlantillaCorreoApiController : Controllers.PlantillaCorreoApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public PlantillaCorreoApiController(ServiceDbContext context)
        {
            _context = context;
        }

        private PlantillaCorreoResponse MapToResponse(PlantillaCorreo p)
        {
            return new PlantillaCorreoResponse
            {
                PlantillaCorreoId = p.PlantillaCorreoId,
                CategoriaPlantillaId = p.CategoriaPlantillaId,
                Nombre = p.Nombre,
                Asunto = p.Asunto,
                CuerpoHtml = p.CuerpoHtml,
                Activa = p.Activa,
                FechaRegistro = p.FechaRegistro,
                FechaActualizacion = p.FechaActualizacion,

                NombreCategoria = p.CategoriaPlantilla != null
                    ? p.CategoriaPlantilla.Nombre
                    : null
            };
        }

        private void MapToPlantilla(PlantillaCorreo plantilla, PlantillaCorreoRequest body)
        {
            if (plantilla == null || body == null) return;

            plantilla.CategoriaPlantillaId = body.CategoriaPlantillaId;
            plantilla.Nombre = body.Nombre;
            plantilla.Asunto = body.Asunto;
            plantilla.CuerpoHtml = body.CuerpoHtml;
            plantilla.Activa = body.Activa ?? true;
            plantilla.FechaActualizacion = DateTime.Now;
        }

        private IQueryable<PlantillaCorreo> QueryCompleta()
        {
            return _context.PlantillaCorreo
                .Include(p => p.CategoriaPlantilla);
        }

        // GET ALL — solo las activas: la baja es lógica.
        public override async Task<IActionResult> GetPlantillasCorreoAsync(string version)
        {
            var plantillas = await QueryCompleta()
                .AsNoTracking()
                .Where(p => p.Activa)
                .OrderBy(p => p.Nombre)
                .ToListAsync();

            return Ok(plantillas.Select(MapToResponse).ToList());
        }

        // GET BY ID
        public override async Task<IActionResult> GetPlantillaCorreoByIdAsync(string version, int idPlantilla)
        {
            var plantilla = await QueryCompleta()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PlantillaCorreoId == idPlantilla);

            if (plantilla == null)
                return NotFound();

            return Ok(MapToResponse(plantilla));
        }

        // CREATE
        public override async Task<IActionResult> CreatePlantillaCorreoAsync(string version, [FromBody] PlantillaCorreoRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(body.Nombre))
                return BadRequest("El nombre de la plantilla es obligatorio");

            var existeCategoria = await _context.CategoriaPlantilla
                .AnyAsync(c => c.CategoriaPlantillaId == body.CategoriaPlantillaId);

            if (!existeCategoria)
                return BadRequest("La categoría no existe");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var plantilla = new PlantillaCorreo
                {
                    FechaRegistro = DateTime.Now
                };

                MapToPlantilla(plantilla, body);

                _context.PlantillaCorreo.Add(plantilla);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var creada = await QueryCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PlantillaCorreoId == plantilla.PlantillaCorreoId);

                return Ok(MapToResponse(creada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // UPDATE
        public override async Task<IActionResult> UpdatePlantillaCorreoAsync(string version, int idPlantilla, [FromBody] PlantillaCorreoRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plantilla = await _context.PlantillaCorreo
                .FirstOrDefaultAsync(p => p.PlantillaCorreoId == idPlantilla);

            if (plantilla == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(body.Nombre))
                return BadRequest("El nombre de la plantilla es obligatorio");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                MapToPlantilla(plantilla, body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var actualizada = await QueryCompleta()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PlantillaCorreoId == idPlantilla);

                return Ok(MapToResponse(actualizada));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // DELETE — baja lógica, para no perder el historial de lo ya enviado.
        public override async Task<IActionResult> DeletePlantillaCorreoAsync(string version, int idPlantilla)
        {
            var plantilla = await _context.PlantillaCorreo
                .FirstOrDefaultAsync(p => p.PlantillaCorreoId == idPlantilla);

            if (plantilla == null)
                return NotFound(new { message = "La plantilla no existe" });

            try
            {
                plantilla.Activa = false;
                plantilla.FechaActualizacion = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Plantilla eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la plantilla", detail = ex.Message });
            }
        }

        // CATALOGO DE CATEGORIAS
        public override async Task<IActionResult> GetCategoriasPlantillaAsync(string version)
        {
            var categorias = await _context.CategoriaPlantilla
                .AsNoTracking()
                .OrderBy(c => c.CategoriaPlantillaId)
                .ToListAsync();

            var response = categorias.Select(c => new CategoriaPlantillaResponse
            {
                CategoriaPlantillaId = c.CategoriaPlantillaId,
                Nombre = c.Nombre ?? string.Empty,
                EsSistema = c.EsSistema
            }).ToList();

            return Ok(response);
        }
    }
}
