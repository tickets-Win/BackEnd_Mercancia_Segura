//using System.Threading.Tasks;
//using Asp.Versioning;
//using MercanciaSegura.RestAPI.Controllers.Base;
//using MercanciaSegura.RestAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Annotations;

//[ApiController]
//[ApiVersion("1.0")]
//public abstract class ClienteApi : ServiceBaseController
//{
//    [HttpPost]
//    [Route("/{version:apiVersion}/clientes")]
//    [SwaggerOperation("Crear cliente")]
//    [SwaggerResponse(201, "Cliente creado", typeof(ClienteResponse))]
//    [SwaggerResponse(400, "Datos inválidos")]
//    public abstract Task<IActionResult> CrearCliente(
//        [FromRoute] string version,
//        [FromBody] CreateClienteRequest request);
//}
