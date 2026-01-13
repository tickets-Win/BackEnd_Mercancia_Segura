using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models;

public class LoginRequest
{
    [Required]
    public string usuario { get; set; } = string.Empty;

    [Required]
    public string password { get; set; } = string.Empty;
}