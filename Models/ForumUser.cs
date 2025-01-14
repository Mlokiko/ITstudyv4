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
    }
}
