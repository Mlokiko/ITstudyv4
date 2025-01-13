using System.ComponentModel.DataAnnotations;

namespace ITstudyv4.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [Display(Name = "Nazwa użytkownika")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [Display(Name = "Imię")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła nie zgadzają się.")]
        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? ProfilePictureURL { get; set; }  
    }
}
