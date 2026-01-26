using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string correo { get; set; } = string.Empty;

    [Required]
    public string password { get; set; } = string.Empty;
}