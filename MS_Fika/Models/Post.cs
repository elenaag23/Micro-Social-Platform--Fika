using System.ComponentModel.DataAnnotations;

namespace MS_Fika.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Continutul postarii este obligatoriu")]
        public string PostContent { get; set; }
        public DateTime PostDate { get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
