using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MercanciaSegura.RestAPI.Models;

namespace MercanciaSegura.RestAPI.Controllers
{
    [ApiController]
    public abstract class AuthApi : ServiceBaseController
    {
        /// <summary>
        /// Login con credenciales
        /// </summary>
        [HttpPost]
        [Route("/{version:apiVersion}/auth/login")]
        [Consumes("application/json")]
        [SwaggerOperation("Login")]
        [SwaggerResponse(200, Description = "OK")]
        [SwaggerResponse(401, Description = "Response to client error satus code")]
        public abstract Task<IActionResult> Login(
            [FromRoute]
            string version,

            [FromBody]
            LoginRequest request);
    }
}
