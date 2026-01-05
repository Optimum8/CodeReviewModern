using CodeReviewModern.Data;
using CodeReviewModern.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodeReviewModern.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<ReviewComment> InitialMessages { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            InitialMessages = await _context.ReviewComments
        .OrderByDescending(m => m.CreatedAt)
        .Take(50)
        .OrderBy(x => x.CreatedAt)
        .ToListAsync();
        }

        public async Task<IActionResult> OnGetMessagesAsync()
        {
            var messages = await _context.ReviewComments
                .OrderByDescending(m => m.CreatedAt)
                .Take(50)
                .ToListAsync();
           
            return Partial("_ChatMessages", messages.OrderBy(x => x.CreatedAt).ToList());          
        }


        public async Task<IActionResult> OnPostPostMessageAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content) || !User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);

            var comment = new ReviewComment
            {
                Content = content,
                UserName = user?.Handle ?? "Anonymous",
                CreatedAt = DateTime.UtcNow
            };

            _context.ReviewComments.Add(comment);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}