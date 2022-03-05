using Microsoft.EntityFrameworkCore;
using OutboxPatternSample.Domain;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasKey(c=> c.Id);
        base.OnModelCreating(modelBuilder);
    }
}