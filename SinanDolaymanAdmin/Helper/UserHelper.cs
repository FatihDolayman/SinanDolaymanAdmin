using System.Security.Claims;
using System.Security.Principal;

namespace SinanDolaymanAdmin.Helper
{
    public static class UserHelper
    {
        public static string GetFullName(this IPrincipal user)
        {
            var fullNameClaim = ((ClaimsIdentity)user.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;
            return "";
        }


        public static string GetUserId(this IPrincipal user)
        {
            var IdClaim = ((ClaimsIdentity)user.Identity).FindFirst("Id");
            if (IdClaim != null)
                return IdClaim.Value;
            return null;
        }
    }
}