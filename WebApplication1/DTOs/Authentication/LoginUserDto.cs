using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.DTOs.Authentication;

public class LoginUserDto
{
    [Required(ErrorMessage = "Email id is required"), EmailAddress(ErrorMessage = "Email id is invalid"),
     MaxLength(120, ErrorMessage = "Maximum length of email id should be 120")]
    [JsonPropertyName("email")]
    public string EmailId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required"),
     MinLength(8, ErrorMessage = "Minimum length of password must be 8"),
     MaxLength(52, ErrorMessage = "Maximum length of password must be 52")]
    public string Password { get; set; } = string.Empty;
}