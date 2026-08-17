using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class PlantillaCorreoApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/plantillaCorreo")]
        [ValidateModelState]
        [SwaggerOperation("GetPlantillasCorreo")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlantillaCorreoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetPlantillasCorreoAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/plantillaCorreo/{idPlantilla}")]
        [ValidateModelState]
        [SwaggerOperation("GetPlantillaCorreoById")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlantillaCorreoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetPlantillaCorreoByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idPlantilla);

        [HttpPost]
        [Route("/{version:apiVersion}/plantillaCorreo")]
        [ValidateModelState]
        [SwaggerOperation("CreatePlantillaCorreo")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlantillaCorreoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> CreatePlantillaCorreoAsync(
            [FromRoute][Required] string version,
            [FromBody] PlantillaCorreoRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/plantillaCorreo/{idPlantilla}")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePlantillaCorreo")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlantillaCorreoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> UpdatePlantillaCorreoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idPlantilla,
            [FromBody] PlantillaCorreoRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/plantillaCorreo/{idPlantilla}")]
        [ValidateModelState]
        [SwaggerOperation("DeletePlantillaCorreo")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> DeletePlantillaCorreoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idPlantilla);

        [HttpGet]
        [Route("/{version:apiVersion}/categoriaPlantilla")]
        [ValidateModelState]
        [SwaggerOperation("GetCategoriasPlantilla")]
        [SwaggerResponse(statusCode: 200, type: typeof(CategoriaPlantillaResponse), description: "OK")]
        public abstract Task<IActionResult> GetCategoriasPlantillaAsync(
            [FromRoute][Required] string version);
    }
}
