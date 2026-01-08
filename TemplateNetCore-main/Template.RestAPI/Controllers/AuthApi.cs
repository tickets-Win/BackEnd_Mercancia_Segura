using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Template.RestAPI.Controllers.Base;
using Template.RestAPI.Models;

namespace Template.RestAPI.Controllers
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
