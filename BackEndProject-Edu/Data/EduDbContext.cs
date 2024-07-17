
using BackEndProject_Edu.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Data
{
    public class EduDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderText> SliderTexts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        //public DbSet<CourseBlog> CoursesBlogs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public EduDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
