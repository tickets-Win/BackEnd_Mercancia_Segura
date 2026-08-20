using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Controllers.Catalogos
{
    [ApiController]
    public abstract class MonedaApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/moneda")]
        [ValidateModelState]
        [SwaggerOperation("GetMoneda")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetMonedaApiAsync(
            [FromRoute][Required] string version);

        [HttpPut]
        [Route("/{version:apiVersion}/moneda/{idMoneda}/tipoCambio")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTipoCambio")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> UpdateTipoCambioAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idMoneda,
            [FromBody] TipoCambioRequest body);
    }
}
