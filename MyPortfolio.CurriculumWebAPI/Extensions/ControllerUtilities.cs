using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Extensions
{
    public static class ControllerUtilities
    {
        public static async Task<string> GetEmailFromUser(this ControllerBase controller)
        {
            var claims = controller.User.Claims.ToList();
            var email = claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
            return email;
        }
    }
}
