using DegreePlanner.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DegreePlanner.Data;

public class DegreePlannerContext(DbContextOptions<DegreePlannerContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<AssignmentCategory> AssignmentCategories => Set<AssignmentCategory>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Degree> Degrees => Set<Degree>();
    public DbSet<Prerequisite> Prerequisites => Set<Prerequisite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prerequisite>()
            .HasOne(prerequisite => prerequisite.Course)
            .WithMany(course => course.Prerequisites)
            .HasForeignKey(prerequisite => prerequisite.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Prerequisite>()
            .HasOne(prerequisite => prerequisite.PrerequisiteCourse)
            .WithMany(course => course.CoursesRequiringThis)
            .HasForeignKey(prerequisite => prerequisite.PrerequisiteCourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}