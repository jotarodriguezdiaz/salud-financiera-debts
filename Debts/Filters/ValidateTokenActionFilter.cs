using Debts.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Debts.API.Filters
{
    public class ValidateTokenActionFilter : IAsyncActionFilter
    {
        private readonly JwtSettings _jwtSettings;

        public ValidateTokenActionFilter(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);

                // Obtén los claims del token válido
                IEnumerable<Claim> claims = GetClaimsFromToken(token);

                // Guarda los claims en el HttpContext
                context.HttpContext.Items["UserClaims"] = claims;

                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new { message = "Unauthorized" });
            }
        }

        private bool IsValidToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Reemplaza "Bearer " para obtener el token en sí.
            token = token.Replace("Bearer ", "");

            // Configura tus opciones de validación de token aquí.
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                // Crea un nuevo JwtSecurityTokenHandler y valida el token.
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

                return true;
            }
            catch (Exception)
            {
                // Si ocurre alguna excepción durante la validación del token, considera que no es válido.
                return false;
            }
        }

        private IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
    }
}
