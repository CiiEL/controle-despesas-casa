using Microsoft.EntityFrameworkCore;
using ControleDespesasCasa.Api.Models;



namespace ControleDespesasCasa.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Category> Categories { get; set; } 
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Person)
            .WithMany(p => p.Transactions)
            .HasForeignKey(ft => ft.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(ft => ft.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
