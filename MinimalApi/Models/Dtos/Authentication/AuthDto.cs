using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Models.Dtos.Authentication;

public class AuthDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Code { get; set; }
}
