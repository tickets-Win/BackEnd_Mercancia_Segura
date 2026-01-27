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
    public abstract class RfcGenericoApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/rfcGenerico")]
        [ValidateModelState]
        [SwaggerOperation("GetRfcGenericoApi")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400),
        description: "Response to client error satus code")]
        public abstract Task<IActionResult> GetRfcGenericoApiAsync(
            [FromRoute][Required] string version);
    }
}
