using System.ComponentModel.DataAnnotations;

namespace MS_Fika.Models
{
    public class Profile
    {
        [Key]

        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Profilul trebuie sa aiba un nume")]
        [StringLength(25, ErrorMessage = "Numele profilului nu poate avea mai mult de 25 de caractere")]
        public string ProfileName { get; set; }

        public string ProfileUsername { get; set; }

        [StringLength(100, ErrorMessage = "Descrierea profilului nu poate avea mai mult de 100 de caractere")]
        public string? ProfileBio { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public bool ProfilePublic { get; set; }

        //public virtual ICollection<Group>? Groups { get; set; }


    }
}
