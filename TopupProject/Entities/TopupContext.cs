using Microsoft.EntityFrameworkCore;
using TopupProject.Entities;

public class TopupContext : DbContext
{
    public TopupContext(DbContextOptions<TopupContext> options)
        : base(options)
    {
    }

    public DbSet<Beneficiary> Beneficiaries { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Topup> Topups { get; set; }
}

