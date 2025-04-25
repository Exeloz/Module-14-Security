using Microsoft.EntityFrameworkCore;
using LoginApp.Model;
using System.IO;
using System.Configuration;
using LoginApp.Utils.Services.Interfaces;

public class ApplicationDbContext : DbContext
{
    private readonly IConfigurationService _configurationService;

    public ApplicationDbContext(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    protected override void OnConfiguring(
       DbContextOptionsBuilder optionsBuilder)
    {
        var rawDbPath = ConfigurationManager.AppSettings["DbPath"];
        var resolvedDbPath = Environment.ExpandEnvironmentVariables(rawDbPath);

        Directory.CreateDirectory(Path.GetDirectoryName(resolvedDbPath));
        var connectionString = $"Data Source={resolvedDbPath}";

        optionsBuilder.UseSqlite(connectionString);
    }

    public DbSet<User> Users { get; set; }

    public void SeedData()
    {
        if (!Users.Any())
        {
            var hashedPassword1 = BCrypt.Net.BCrypt.HashPassword(_configurationService.GetDefaultAdminPassword());
            var user1 = new User { Email = _configurationService.GetDefaultAdminUserName(), Password = hashedPassword1 };

            Users.AddRange(user1);

            SaveChanges();
        }

    }
}
