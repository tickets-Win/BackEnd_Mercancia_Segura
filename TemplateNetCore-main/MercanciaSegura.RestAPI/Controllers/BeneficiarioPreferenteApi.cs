using System.Collections.Generic;
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
    public abstract class BeneficiarioPreferenteApiControllerBase : ServiceBaseController
    {
        // GET ALL
        [HttpGet]
        [Route("/{version:apiVersion}/beneficiarioPreferente")]
        [ValidateModelState]
        [SwaggerOperation("GetBeneficiariosPreferentes")]
        [SwaggerResponse(statusCode: 200, type: typeof(IEnumerable<BeneficiarioPreferenteResponse>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Client error")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        public abstract Task<IActionResult> GetBeneficiarioPreferenteAsync(
            [FromRoute][Required] string version);


        // GET BY ID
        [HttpGet]
        [Route("/{version:apiVersion}/beneficiarioPreferente/{idBeneficiarioPreferente}")]
        [ValidateModelState]
        [SwaggerOperation("GetBeneficiarioPreferenteById")]
        [SwaggerResponse(statusCode: 200, type: typeof(BeneficiarioPreferenteResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Client error")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        public abstract Task<IActionResult> GetBeneficiarioPreferenteByIdAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int id);


        // POST
        [HttpPost]
        [Route("/{version:apiVersion}/beneficiarioPreferente")]
        [ValidateModelState]
        [SwaggerOperation("CreateBeneficiarioPreferente")]
        [SwaggerResponse(statusCode: 200, type: typeof(BeneficiarioPreferenteResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Client error")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        public abstract Task<IActionResult> CreateBeneficiarioPreferenteAsync(
            [FromRoute][Required] string version,
            [FromBody][Required] BeneficiarioPreferenteRequest body);


        // PUT
        [HttpPut]
        [Route("/{version:apiVersion}/beneficiarioPreferente/{idBeneficiarioPreferente}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateBeneficiarioPreferente")]
        [SwaggerResponse(statusCode: 200, type: typeof(BeneficiarioPreferenteResponse), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Client error")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        public abstract Task<IActionResult> UpdateBeneficiarioPreferenteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int id,
            [FromBody][Required] BeneficiarioPreferenteRequest body);


        // DELETE (Soft Delete)
        [HttpDelete]
        [Route("/{version:apiVersion}/beneficiarioPreferente/{idBeneficiarioPreferente}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteBeneficiarioPreferente")]
        [SwaggerResponse(statusCode: 200, description: "Deleted successfully")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Client error")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        public abstract Task<IActionResult> DeleteBeneficiarioPreferenteAsync(
            [FromRoute][Required] string version,
            [FromRoute][Required] int id);
    }
}