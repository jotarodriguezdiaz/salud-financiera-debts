using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Debts.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Guid GetUserIdFromToken()
        {
            if (HttpContext.Items.TryGetValue("UserClaims", out object claimsObject) && claimsObject is IEnumerable<Claim> claims)
            {
                var jtiClaim = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti);
                if (jtiClaim != null)
                {
                    if (Guid.TryParse(jtiClaim.Value, out Guid userId))
                    {
                        return userId;
                    }
                }
            }

            throw new InvalidOperationException("Unable to retrieve user ID from the JWT token.");
        }
    }
}
