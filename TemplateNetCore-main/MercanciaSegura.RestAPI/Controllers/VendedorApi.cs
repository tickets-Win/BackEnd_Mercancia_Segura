using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Vendedor;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class VendedorApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/vendedor")]
        [ValidateModelState]
        [SwaggerOperation("GetVendedores")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> GetVendedoresAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/vendedor/{idVendedor}")]
        [ValidateModelState]
        [SwaggerOperation("GetVendedorById")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> GetVendedorByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idVendedor);

        [HttpGet]
        [Route("/{version:apiVersion}/vendedor/tipo{tipoVendedorId}")]
        [ValidateModelState]
        [SwaggerOperation("GetVendedorById")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> GetVendedorByTipoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int tipoVendedorId);

        [HttpPost]
        [Route("/{version:apiVersion}/vendedor")]
        [ValidateModelState]
        [SwaggerOperation("CreateVendedor")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> CreateVendedorAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] VendedorRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/vendedor/{idVendedor}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateVendedor")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> UpdateVendedorAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idVendedor,
            [FromBody][Required] VendedorRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/vendedor/{idVendedor}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteVendedor")]
        [SwaggerResponse(statusCode: 200, type: typeof(VendedorRequest), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
            description: "Response to client error status code")]
        public abstract Task<IActionResult> DeleteVendedorAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idVendedor);
    }
}
