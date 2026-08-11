using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Attributes;
using MercanciaSegura.RestAPI.Controllers.Base;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class BienApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/bien/poliza/{polizaId}")]
        [ValidateModelState]
        [SwaggerOperation("GetBienByPoliza")]
        [SwaggerResponse(statusCode: 200, description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetBienByPolizaAsync(
    [FromRoute][Required] string version,
    [FromRoute][Required] int polizaId);
    }
}