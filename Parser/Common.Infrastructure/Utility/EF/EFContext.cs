using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Utility.EF;

public class EFContext : DbContext
{
    public EFContext(IOptions<EFConfig> config)
    {
        Config = config.Value;
    }

    private EFConfig Config { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStrings = EFConnectionStringsBuilder.Builder
            .SetServer(Config.Server)
            .SetPort(Config.Port)
            .SetDatabase(Config.Database)
            .SetUser(Config.User)
            .SetPassword(Config.Password)
            .Build();
        
        optionsBuilder.UseMySql(connectionStrings, new MySqlServerVersion(new Version(8, 0, 23)), 
            options => options.EnableRetryOnFailure());
        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}