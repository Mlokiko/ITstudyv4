namespace ITstudyv4.Models
{
    public class Posts
    {
        public int Id { get; set; } // Entinty framework wymaga Id, żeby uruchomić samo numeracje, potem się sprawdzi jak to pod spodem działa
        public string? Content { get; set; } // W .sql mamy TEXT, mam nadzieje że string będzie działać
        public DateTime CreatedDate { get; set; } // Jak dodać defaultowe wartości?
        public bool Edited { get; set; } // znowu, jak dodać defaultowe (0, dla nie edytowanego)?
        public string? UserId { get; set; }
        public int ThreadId { get; set; }

        // Powinno podłączyć/powiązać z inną tabelą
        public virtual ForumUser? User { get; set; }
        public virtual Threads? Thread { get; set; }
    }
}
