using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MS_Fika.Models;

namespace MS_Fika.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<UserInGroup> UserInGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // definire primary key compus
            modelBuilder.Entity<UserInGroup>()
                .HasKey(ab => new { ab.Id, ab.UserId, ab.GroupId });


            // definire relatii cu modelele Bookmark si Article (FK)
            modelBuilder.Entity<UserInGroup>()
                .HasOne(ab => ab.User)
                .WithMany(ab => ab.UserInGroups)
                .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<UserInGroup>()
                .HasOne(ab => ab.Group)
                .WithMany(ab => ab.UserInGroups)
                .HasForeignKey(ab => ab.GroupId);
        }


    }
}