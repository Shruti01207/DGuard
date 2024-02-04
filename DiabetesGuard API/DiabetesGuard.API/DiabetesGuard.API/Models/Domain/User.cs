using Microsoft.AspNetCore.Identity;

namespace DiabetesGuard.API.Models.Domain
{
    public class User: IdentityUser
    {

        public string RefreshToken { get; set; }=string.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
