using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class BaseContext : IdentityDbContext<ApplicationUser>
{
    public BaseContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<FinancialSystem> FinancialSystems { get; set; }
    public DbSet<FinancialSystemUser> FinancialSystemUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
        base.OnModelCreating(builder);
    }

    public string GetConnectionString()
    {
        return "Data Source=localhost,1433;Initial Catalog=FinancialSystem2023;Integrated Security=False;User Id=SA;Password=P@ssword123;";
    }
}
