using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Models.Dtos.Authentication;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
