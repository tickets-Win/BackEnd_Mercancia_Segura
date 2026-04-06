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
    public abstract class CertificadoApiControllerBase : ServiceBaseController
    {
        [HttpGet]
        [Route("/{version:apiVersion}/certificado")]
        [ValidateModelState]
        [SwaggerOperation("GetCertificados")]
        [SwaggerResponse(statusCode: 200, type: typeof(CertificadoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400))]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetCertificadosAsync(
            [FromRoute][Required] string version);

        [HttpGet]
        [Route("/{version:apiVersion}/certificado/{idCertificado}")]
        [ValidateModelState]
        [SwaggerOperation("GetCertificadoById")]
        [SwaggerResponse(statusCode: 200, type: typeof(CertificadoResponse), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400))]
        public abstract Task<IActionResult> GetCertificadoByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCertificado);

        [HttpPost]
        [Route("/{version:apiVersion}/certificado")]
        [ValidateModelState]
        [SwaggerOperation("CreateCertificado")]
        [SwaggerResponse(statusCode: 201, type: typeof(CertificadoResponse), description: "Created")]
        public abstract Task<IActionResult> CreateCertificadoAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] CertificadoRequest body);

        [HttpPut]
        [Route("/{version:apiVersion}/certificado/{idCertificado}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateCertificado")]
        [SwaggerResponse(statusCode: 200, type: typeof(CertificadoResponse), description: "Updated")]
        public abstract Task<IActionResult> UpdateCertificadoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCertificado,
            [FromBody][Required] CertificadoRequest body);

        [HttpDelete]
        [Route("/{version:apiVersion}/certificado/{idCertificado}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteCertificado")]
        [SwaggerResponse(statusCode: 200, description: "Deleted")]
        public abstract Task<IActionResult> DeleteCertificadoAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int idCertificado);
    }
}