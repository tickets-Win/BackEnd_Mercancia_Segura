using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class CorreosApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/cliente/{clienteId}/correos")]
        [ValidateModelState]
        [SwaggerOperation("GetCorreosByCliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(CorreoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> GetCorreosByClienteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int clienteId);
    }
}
