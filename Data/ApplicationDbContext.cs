using MamNonApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MamNonApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Các DbSet cho entities
        public DbSet<EducationProgram> Programs { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Activity> Activities { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public DbSet<GalleryItem> GalleryItems { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public DbSet<SchoolInfo> SchoolInfos { get; set; } = null!;
        public DbSet<Testimonial> Testimonials { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
            modelBuilder.Entity<EducationProgram>().ToTable("EducationPrograms");
            modelBuilder.Entity<News>().ToTable("News");
        }
    }
}
