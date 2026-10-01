using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FirstWebAppInDocker.DataAccess
{
    // Design-time factory used by EF Core tools (dotnet ef / Update-Database)
    // This ensures the tools can create the DbContext when running on the developer machine
    // where the DB host inside Docker (mariadbcontainer) is not resolvable.
    // Strategy:
    //  - Read connection string from appsettings.json and environment variables
    //  - If it references the internal Docker host name (mariadbcontainer), replace it with
    //    127.0.0.1 so tools run on the host can connect to the container's published port (3306)
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Use the project directory where this code lives as base path so appsettings.json can be read
            var basePath = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection")
                                   ?? "Server=127.0.0.1;Port=3306;Database=resourcesdb;User=root;Password=myDB123;";

            // If the connection string uses the Docker service name, replace it so design-time tools
            // running on the host can connect to the mapped 127.0.0.1:3306 port.
            if (connectionString.Contains("mariadbcontainer", StringComparison.OrdinalIgnoreCase) ||
                connectionString.Contains("mariadbcontaine", StringComparison.OrdinalIgnoreCase))
            {
                connectionString = connectionString.Replace("mariadbcontainer", "127.0.0.1", StringComparison.OrdinalIgnoreCase)
                                                   .Replace("mariadbcontaine", "127.0.0.1", StringComparison.OrdinalIgnoreCase);
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
