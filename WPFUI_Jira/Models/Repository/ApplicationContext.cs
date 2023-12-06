using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFUI_Jira.Models.Repository;

internal class ApplicationContext : DbContext
{
    private string _connectionString = ConfigurationManager.ConnectionStrings["TestDBConnection"].ConnectionString;

    public DbSet<User> Users { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<TaskBoard> TaskBoards { get; set; }

    public DbSet<TaskList> TaskLists { get; set; }

    public DbSet<TaskCard> TaskCards { get; set; }

    public ApplicationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(c => c.WorkProjects)
            .WithMany(s => s.Workers)
            .UsingEntity(j => j.ToTable("ProjectWorkers"));

        modelBuilder.Entity<User>()
            .HasMany(c => c.OwnedProjects)
            .WithOne(s => s.Owner);

        modelBuilder
            .Entity<TaskList>()
            .Property(c => c.Type)
            .HasConversion(
                v => v.ToString(),
                v => (ListType)Enum.Parse(typeof(ListType), v));
    }
}
