using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Poliza;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MercanciaSegura.RestAPI.Controllers.Catalogos
{
    [ApiController]
    public abstract class PolizaApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/poliza")]
        [ValidateModelState]
        [SwaggerOperation("GetPolizas")]
        [SwaggerResponse(statusCode: 200, type: typeof(PolizaResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetPolizaAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/poliza/{polizaId}")]
        [ValidateModelState]
        [SwaggerOperation("GetPolizaById")]
        [SwaggerResponse(statusCode: 200, type: typeof(PolizaResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetPolizaByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int polizaId);

        [HttpPost]
        [Route("/{version:apiVersion}/poliza")]
        [ValidateModelState]
        [SwaggerOperation("CreatePoliza")]
        [SwaggerResponse(statusCode: 201, type: typeof(PolizaResponse), description: "Created")]
        public abstract Task<IActionResult> CreatePolizaAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] PolizaRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/poliza/{polizaId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePoliza")]
        [SwaggerResponse(statusCode: 200, type: typeof(PolizaResponse), description: "Updated")]
        public abstract Task<IActionResult> UpdatePolizaAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int polizaId,
            [FromBody][Required] PolizaRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/poliza/{polizaId}")]
        [ValidateModelState]
        [SwaggerOperation("DeletePoliza")]
        [SwaggerResponse(statusCode: 200, description: "Deleted")]
        public abstract Task<IActionResult> DeletePolizaAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int polizaId);
    }
}
