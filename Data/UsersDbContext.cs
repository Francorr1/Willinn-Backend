using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "AQAAAAIAAYagAAAAEHR/TT++jIdACkme63c/hXYQGOimCcg//+z34xsAdpXZMEesJw8nmfue+p9e5l3sRw==",
            IsActive = true
        });
    }
}