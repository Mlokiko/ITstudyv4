using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ITstudyv4.Models
{
    public class ForumUser:IdentityUser
    {
        // ID, email, username wykorzystujemy z Identity
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        public DateTime JoinDate { get; set; }
        [MaxLength(200)]
        public string? ProfilePictureURL { get; set; }
        public string? Bio { get; set; }
        public int RankId { get; set; }

        public virtual Ranks Rank { get; set; }  // I guess ze nie potrzebne, w AccountControler nie jest wykorzystywane, tutaj wsm też, potem może będą z tego powodu problemy, no ale cóż, pozyjemy zobaczymy

    }
}
