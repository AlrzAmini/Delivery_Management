using DriversManagement.Models.Data.Entities;
using System.Security.Claims;

namespace DriversManagement.Utilities.ExtensionMethods
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var strUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return strUserId == null ? 0 : Convert.ToInt32(strUserId);
        }
    }
}
