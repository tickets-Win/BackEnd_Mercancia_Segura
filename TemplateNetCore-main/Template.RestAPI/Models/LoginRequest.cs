using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class LoginRequest
{
    [Required]
    public string usuario { get; set; } = string.Empty;

    [Required]
    public string password { get; set; } = string.Empty;
}
