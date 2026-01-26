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
    public abstract class ClienteApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/cliente")]
        [ValidateModelState]
        [SwaggerOperation("GetCliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClienteRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> GetClientesAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/cliente/{idCliente}")]
        [ValidateModelState]
        [SwaggerOperation("GetClienteById")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClienteRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> GetClienteByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCliente);

        [HttpPost]
        [Route("/{version:apiVersion}/cliente")]
        [ValidateModelState]
        [SwaggerOperation("CreateCliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClienteRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> CreateClienteAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] ClienteRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/cliente/{idCliente}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateCliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClienteRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> UpdateClienteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCliente,
            [FromBody][Required] ClienteRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/cliente/{idCliente}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteCliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClienteRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> DeleteClienteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCliente);
    }
}
