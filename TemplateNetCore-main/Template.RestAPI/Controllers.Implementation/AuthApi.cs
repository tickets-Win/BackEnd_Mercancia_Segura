using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Template.Funcionalidad.Services;
using Template.RestAPI.Models;

namespace Template.RestAPI.Controllers.Implementation
{
    public class AuthApi : Controllers.AuthApi
    {
        private readonly AuthService _authService;
        private readonly string _jwtKey = "EstaEsMiClaveSuperSecreta123!";

        public AuthApi(AuthService authService)
        {
            _authService = authService;
        }

        public override async Task<IActionResult> Login(
            string version,
            LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.LoginAsync(
                request.usuario,
                request.password);

            if (user == null)
                return Unauthorized("Usuario no encontrado o credenciales inválidas");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Usuario_ID", user.UsuarioId.ToString()),
                    new Claim("Usuario", user.UsuarioNombre)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                usuarioId = user.UsuarioId,
                usuarioNombre = user.UsuarioNombre
            });
        }
    }
}
