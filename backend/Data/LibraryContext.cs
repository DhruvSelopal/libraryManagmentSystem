using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<IssueRecord> IssueRecords { get; set; }
}