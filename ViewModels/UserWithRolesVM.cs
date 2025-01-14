namespace ITstudyv4.ViewModels
{
    public class UserWithRolesVM
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? JoinDate { get; set; }
        //public IEnumerable<string> Roles { get; set; } // Zakładamy że użytkownik może mieć tylko jedną rolę, dlatego tego nie używamy
        public string? Role { get; set; }
    }
}
