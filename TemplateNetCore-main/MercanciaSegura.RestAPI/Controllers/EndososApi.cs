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
    public abstract class EndososApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/endosos")]
        [ValidateModelState]
        [SwaggerOperation("GetEndosos")]
        [SwaggerResponse(statusCode: 200, type: typeof(EndososResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetEndososAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/endosos/{idEndoso}")]
        [ValidateModelState]
        [SwaggerOperation("GetEndosoById")]
        [SwaggerResponse(statusCode: 200, type: typeof(EndososResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetEndosoByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idEndoso);

        [HttpPost]
        [Route("/{version:apiVersion}/endosos")]
        [ValidateModelState]
        [SwaggerOperation("CreateEndoso")]
        [SwaggerResponse(statusCode: 201, type: typeof(EndososResponse), description: "Created")]
        public abstract Task<IActionResult> CreateEndosoAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] EndososRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/endosos/{idEndoso}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateEndoso")]
        [SwaggerResponse(statusCode: 200, type: typeof(EndososResponse), description: "Updated")]
        public abstract Task<IActionResult> UpdateEndosoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idEndoso,
            [FromBody][Required] EndososRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/endosos/{idEndoso}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteEndoso")]
        [SwaggerResponse(statusCode: 200, description: "Deleted")]
        public abstract Task<IActionResult> DeleteEndosoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idEndoso);
    }
}