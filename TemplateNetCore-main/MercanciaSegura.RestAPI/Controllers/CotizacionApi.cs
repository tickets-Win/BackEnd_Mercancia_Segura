using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class CotizacionApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/cotizacion")]
        [ValidateModelState]
        [SwaggerOperation("GetCotizaciones")]
        [SwaggerResponse(statusCode: 200, type: typeof(CotizacionResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetCotizacionAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/cotizacion/{idCotizacion}")]
        [ValidateModelState]
        [SwaggerOperation("GetCotizacionById")]
        [SwaggerResponse(statusCode: 200, type: typeof(CotizacionResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetCotizacionByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCotizacion);

        [HttpPost]
        [Route("/{version:apiVersion}/cotizacion")]
        [ValidateModelState]
        [SwaggerOperation("CreateCotizacion")]
        [SwaggerResponse(statusCode: 201, type: typeof(CotizacionResponse), description: "Created")]
        public abstract Task<IActionResult> CreateCotizacionAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] CotizacionRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/cotizacion/{idCotizacion}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateCotizacion")]
        [SwaggerResponse(statusCode: 200, type: typeof(CotizacionResponse), description: "Updated")]
        public abstract Task<IActionResult> UpdateCotizacionAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCotizacion,
            [FromBody][Required] CotizacionRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/cotizacion/{idCotizacion}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteCotizacion")]
        [SwaggerResponse(statusCode: 200, description: "Deleted")]
        public abstract Task<IActionResult> DeleteCotizacionAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCotizacion);
    }
}