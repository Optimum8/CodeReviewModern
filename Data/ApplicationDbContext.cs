using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using CodeReviewModern.Models;

namespace CodeReviewModern.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReviewComment> ReviewComments { get; set; } 
    }
}