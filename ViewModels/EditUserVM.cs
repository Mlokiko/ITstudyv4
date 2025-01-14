using System.ComponentModel.DataAnnotations;

namespace ITstudyv4.ViewModels
{
    public class EditUserVM
    {
        public string? UserId { get; set; }

        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Role")]
        public List<string> Roles { get; set; } = new List<string>();

        [Display(Name = "Zdjęcie profilowe")]
        public string? ProfilePictureURL { get; set; }

        [Display(Name = "Opis konta")]
        public string? Bio { get; set; }
    }
}
