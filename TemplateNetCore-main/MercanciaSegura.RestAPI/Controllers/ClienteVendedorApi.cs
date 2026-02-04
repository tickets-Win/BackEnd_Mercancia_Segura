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
    public abstract class ClienteVendedorApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/cliente/{clienteId}/vendedores")]
        [ValidateModelState]
        [SwaggerOperation("GetVendedoresByCliente")]
        [SwaggerResponse(
            statusCode: 200,
            type: typeof(ClienteVendedorResponse),
            description: "OK")]
        [SwaggerResponse(
            statusCode: 400,
            type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(
            statusCode: 401,
            type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(
            statusCode: 404,
            type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> GetVendedoresByClienteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int clienteId);
    }
}
