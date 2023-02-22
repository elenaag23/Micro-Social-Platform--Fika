using System.ComponentModel.DataAnnotations;

namespace MS_Fika.Models
{
    public class Comment
    {
        [Key]

        public int CommentId { get; set; }

        [Required(ErrorMessage = "Continutul comentariului este obligatoriu")]
        public string? CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public int? PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}
