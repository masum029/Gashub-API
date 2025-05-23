﻿using Microsoft.IdentityModel.Tokens;
using Project.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Project.Infrastructure.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiryMinutes;

        public TokenGenerator(string key, string issuer, string audience, string expiryMinutes)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateJWTToken((string userId, string userName, IList<string> roles) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var (userId, userName, roles) = userDetails;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim("UserId", userId)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiryMinutes)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        public string GenerateSuparAdminJWTToken((string userName, IList<string> roles) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var ( userName, roles) = userDetails;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(ClaimTypes.Name, userName),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiryMinutes)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
    
}
