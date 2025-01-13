using ITstudyv4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITstudyv4.Data
{
    public class AppDbContext : IdentityDbContext<ForumUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Ranks> Ranks { get; set; }
        public DbSet<Threads> Threads { get; set; }
        public DbSet<ForumUser> ForumUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ForumUser>()
            .HasOne(s => s.Rank)
            .WithMany()
            .HasForeignKey(s => s.RankId);
            //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Posts>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Posts>()
            .HasOne(p => p.Thread)
            .WithMany()
            .HasForeignKey(p => p.ThreadId)
            .OnDelete(DeleteBehavior.Cascade);

            // Opis categori wymagany i nie większy od 255, Name wymagane
            builder.Entity<Categories>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<Categories>()
                .Property(c => c.Name)
                .IsRequired();

            // Może zadziałal i ustawi datę prawidłowo, trzeba potem to sprawdzić
            builder.Entity<Posts>()
                .Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Można ustawić max długość postu tutaj, nwm czy chcemy czy nie, raczej chcemy, ale na jaką wartość?
            // Sprawdzić czy będzie działać z dłuższymi wartościami, tzn, czy tworzy wewnętrznie varchara, text czy co w bazie danych
            builder.Entity<Posts>()
                .Property(c => c.Content)
                .IsRequired();

            // Sprawdzić czy ustawia prawidłowo to 0, na czuja to dałem
            builder.Entity<Posts>()
                .Property(c => c.Edited)
                .IsRequired()
                .HasDefaultValue(false);

            // UserId i ThreadID z Posts nie daje tutaj jako required, sprawdzmy czy bez tego działa

            builder.Entity<Ranks>()
                .Property(c => c.Name)
                .IsRequired();

            builder.Entity<Threads>()
                .Property(c => c.Title)
                .IsRequired();

            builder.Entity<Threads>()
                .Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<ForumUser>()
                .Property(c => c.UserName)
                .IsRequired();

            builder.Entity<ForumUser>()
                .Property(c => c.PasswordHash)
                .IsRequired();

            // Jak wymusić żeby było unique?
            builder.Entity<ForumUser>()
                .Property(c => c.Email)
                .IsRequired();

            builder.Entity<ForumUser>()
                .Property(c => c.JoinDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Raczej rozwiązane przez relacje
            //builder.Entity<SiteForumUser>()
            //    .Property(c => c.Rank)
            //    .IsRequired();

            // Czy to jest potrzebne? chat napisał to w momencie jak probowałem relacje naprawić
            base.OnModelCreating(builder);
        }
    }
}
