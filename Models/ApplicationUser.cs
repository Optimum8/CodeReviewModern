using Microsoft.AspNetCore.Identity;

namespace CodeReviewModern.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Handle { get; set; }
    }
}