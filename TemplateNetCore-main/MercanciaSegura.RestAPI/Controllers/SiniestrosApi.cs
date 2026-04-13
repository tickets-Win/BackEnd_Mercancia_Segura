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
    public abstract class SiniestrosApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/siniestros")]
        [ValidateModelState]
        [SwaggerOperation("GetSiniestros")]
        [SwaggerResponse(statusCode: 200, type: typeof(SiniestrosResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetSiniestrosAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/siniestros/{idSiniestro}")]
        [ValidateModelState]
        [SwaggerOperation("GetSiniestroById")]
        [SwaggerResponse(statusCode: 200, type: typeof(SiniestrosResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetSiniestroByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idSiniestro);

        [HttpPost]
        [Route("/{version:apiVersion}/siniestros")]
        [ValidateModelState]
        [SwaggerOperation("CreateSiniestro")]
        [SwaggerResponse(statusCode: 201, type: typeof(SiniestrosResponse), description: "Created")]
        public abstract Task<IActionResult> CreateSiniestroAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] SiniestrosRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/siniestros/{idSiniestro}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateSiniestro")]
        [SwaggerResponse(statusCode: 200, type: typeof(SiniestrosResponse), description: "Updated")]
        public abstract Task<IActionResult> UpdateSiniestroAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idSiniestro,
            [FromBody][Required] SiniestrosRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/siniestros/{idSiniestro}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteSiniestro")]
        [SwaggerResponse(statusCode: 200, description: "Deleted")]
        public abstract Task<IActionResult> DeleteSiniestroAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idSiniestro);
    }
}