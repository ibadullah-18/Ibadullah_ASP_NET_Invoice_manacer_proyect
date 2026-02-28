using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; } 
    public DbSet<InvoiceRow> InvoiceRows { get; set; } 
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Invoices)
            .WithOne(i => i.Customer)
            .HasForeignKey(i => i.CustomerId);

        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Rows)
            .WithOne(r => r.Invoice)
            .HasForeignKey(r => r.InvoiceId);

        modelBuilder.Entity<Customer>()
            .HasQueryFilter(c => c.DeletedAt == null);

        modelBuilder.Entity<Invoice>()
            .HasQueryFilter(i => i.DeletedAt == null);
    }

}


