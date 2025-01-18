using System.ComponentModel.DataAnnotations;

namespace ITstudyv4.Models
{
    // Redundantnie Required jest tutaj i w dbcontext, ale dzieki temu tutaj wywali błędy userowi przy wprowadzaniu danych
    public class Categories
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może przekroczyć 100 znaków.")]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(255, ErrorMessage = "Opis nie może przekroczyć 255 znaków.")]
        [Display(Name = "Opis")]
        public string? Description { get; set; }
    }
}
