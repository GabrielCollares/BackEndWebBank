using Microsoft.EntityFrameworkCore;

namespace BackendSharp.Users;


public class AppDbContext: DbContext
{
public DbSet<User> Users { get; set; }
   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}

