using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
{
    public LibraryContext CreateDbContext(string[] args)
    {
        // Tell EF where the database is
        DbContextOptionsBuilder<LibraryContext> optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
        optionsBuilder.UseSqlServer("Server=BG-CPU-158;Database=LibraryManagment;User Id=sa;Password=sa123;TrustServerCertificate=True;");

        // Return a new context so EF Core can use it
        return new LibraryContext(optionsBuilder.Options);
    }
}
