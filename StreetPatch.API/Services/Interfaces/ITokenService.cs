using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using StreetPatch.Data.Entities.DTO;

namespace StreetPatch.API.Services.Interfaces
{
    public interface ITokenService
    {
        SigningCredentials GetSigningCredentials();
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        List<Claim> GetClaims(UserLoginDto user);
    }
}
