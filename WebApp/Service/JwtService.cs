using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApp.Service.IService;

namespace WebApp.Service
{
    public class JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string GenerateJwtToken(int userId)
        {
            var secret = _configuration.GetValue<string>("ApiSettings:Secreta");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int GetUserIdFromToken(string token)
        {
            var secret = _configuration.GetValue<string>("ApiSettings:Secreta");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string GetTokenFromHeader()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }
    }
}