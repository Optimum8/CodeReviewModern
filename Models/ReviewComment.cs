using System.ComponentModel.DataAnnotations;

namespace CodeReviewModern.Models
{
    public class ReviewComment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } 

        public string UserName { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}