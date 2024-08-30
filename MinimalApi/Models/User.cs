﻿namespace MinimalApi.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? TwoStepSecret { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public ICollection<Rol> Rols { get; set; } = new HashSet<Rol>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    public ICollection<UserRol>? UsersRols { get; set; }
}
