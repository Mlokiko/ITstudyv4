using System.ComponentModel.DataAnnotations;

namespace ITstudyv4.Models
{
    public class Threads
    {
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        public string? Title { get; set; }
        [Display(Name = "Utworzono")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Wyświetlenia")]
        public int Views { get; set; }
        public string? UserId { get; set; } // nie używamy tego - używamy tego virtual ForumUser user 
        public int CategoryId { get; set; }
        public int? AnswersId { get; set; }

        public virtual ForumUser? User { get; set; }
        [Display(Name = "Kategoria")]
        public virtual Categories? Category { get; set; }
        public virtual Posts? Answers { get; set; }
    }
}
