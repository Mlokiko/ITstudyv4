namespace ITstudyv4.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Edited { get; set; }
        public string? UserId { get; set; }
        public int ThreadId { get; set; }
        public virtual ForumUser? User { get; set; }
        public virtual Threads? Thread { get; set; }
    }
}
