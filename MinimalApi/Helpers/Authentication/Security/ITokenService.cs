using MinimalApi.Models.Entities;

namespace MinimalApi.Helpers.Authentication.Security;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}

