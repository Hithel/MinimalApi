﻿namespace MinimalApi.Models;

public class Rol
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRol>? UsersRols { get; set; }
}
