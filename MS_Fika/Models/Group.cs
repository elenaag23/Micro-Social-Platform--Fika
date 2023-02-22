using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS_Fika.Models
{
    public class Group
    {
        [Key]

        public int GroupId { get; set; }

        [Required(ErrorMessage = "Numele grupului este obligatoriu")]
        [StringLength(25, ErrorMessage = "Numele grupului nu poate avea mai mult de 25 de caractere")]
        public string? GroupName { get; set; }

        public DateTime GroupDate { get; set; }

        public string? Description { get; set; }

        public string? GroupAdminId { get; set; }

        public virtual ApplicationUser? GroupAdminUser { get; set; }

        public virtual ICollection<UserInGroup>? UserInGroups { get; set; }

        //public virtual ApplicationUser GroupAdminUser { get; set; }

        //public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }


    }
}
