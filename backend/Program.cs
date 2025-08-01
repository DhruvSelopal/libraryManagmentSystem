using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// // ✅ Add DbContext
// builder.Services.AddDbContext<LibraryContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // ✅ Add controllers
// builder.Services.AddControllers();

// var app = builder.Build()

// app.UseRouting();
// app.MapControllers();

// app.Run();

class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        string? dbUrl = config.GetConnectionString("DefaultConnection");

        DbContextOptions<LibraryContext> options = new DbContextOptionsBuilder<LibraryContext>().UseSqlServer(dbUrl).Options;
        DbContext lib = new LibraryContext(options);

        lib.Add(new User
        {
            UserName = "john_doe",
            FirstName = "John",
            LastName = "Doe",
            PhoneNo = "1234567890",
            UserAddress = "123 Elm Street"
        });

        lib.SaveChanges();

        Console.WriteLine("Executed");
    }
}



