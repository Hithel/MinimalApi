﻿using Microsoft.IdentityModel.Tokens;
using MinimalApi.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApi.Helpers.Authentication.Security;

public class TokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateJwtToken(User user)
    {
        var roleClaims = user.Rols.Select(role => new Claim("roles", role.Nombre)).ToList();

        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim (JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
        }.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
               signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
