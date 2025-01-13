namespace ITstudyv4.Models
{
    public class Threads
    {
        public int Id { get; set; }
        public string? Title { get; set; }  // Not Null, jak to dodać?
        public DateTime CreatedAt { get; set; } // Dodać default aktualny czas 
        public int Views { get; set; }
        public string? UserId { get; set; }
        public int CategoryId { get; set; }
        public int AnswersId { get; set; }

        // W .sql mamy zapisane żeby się kaskadowo usuwało, będzie to trzeba sprawdzić czy prawidłowo się usuwa, nie robiliśmy tego na zajęciach

        public virtual ForumUser? User { get; set; }
        public virtual Categories? Category { get; set; }
        // Answers nie lepiej zamienić na posts? tzn. sama nazwe zminić, żeby się potem nie myliło
        public virtual Posts? Answers { get; set; }
    }
}
