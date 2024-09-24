﻿using MinimalApi.Models.Entities;

namespace MinimalApi.Models.ViewModels.User;

public class UserVm
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
