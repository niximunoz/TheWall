#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace TheWall.Models;


public class MyContext : DbContext 
{   

    public DbSet<User> Users { get; set; } 
    public DbSet<Message> Messages { get; set; } 
    public DbSet<Comment> Comments { get; set; } 
    public MyContext(DbContextOptions options) : base(options) { }    


    
}
