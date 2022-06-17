namespace WebApplicationTemplate.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user) // principal is user
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
