﻿using ProjectStray2HomeAPI.Models.EF;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectStrayToHomeAPI.Handlers.Interfaces
{
    public interface IJwtHandler
    {
        Task<JwtSecurityToken> GetTokenAsync(ApplicationUser user);
        string WriteToken(JwtSecurityToken? secToken);
    }
}
