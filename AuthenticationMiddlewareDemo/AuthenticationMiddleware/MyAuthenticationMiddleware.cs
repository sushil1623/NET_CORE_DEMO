using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationMiddlewareDemo.AuthenticationMiddleware
{
    public class MyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public MyAuthenticationMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var claimsPrincipal = ValidateToken(token);
                if (claimsPrincipal != null)
                {
                    context.User = claimsPrincipal;
                }
            }
            await _next(context);
        }

        private ClaimsPrincipal ValidateToken(string token) {
            var securityTokenHanler = new JwtSecurityTokenHandler();
            var validationParameter = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {

                ValidIssuer = _configuration["JWTSettings:Issuer"],
                ValidAudience = _configuration["JWTSettings:Audience"],
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"])),
                ClockSkew = TimeSpan.Zero
            };

            var claimPrincipals = securityTokenHanler.ValidateToken(token, validationParameter, out SecurityToken securityToken);
            return claimPrincipals;
        }
    }
}
