
using Microsoft.EntityFrameworkCore;
using RamProcessingTool.Entity;
using Microsoft.Extensions.Configuration;


public class ProjectContext : DbContext
{


    private readonly IConfiguration _configuration;

    public ProjectContext(DbContextOptions<ProjectContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration; 
    }
    public DbSet<RamEntity> RamEntities { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(connectionString))
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



    }
}