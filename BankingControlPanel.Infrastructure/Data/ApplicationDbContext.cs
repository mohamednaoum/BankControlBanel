using BankingControlPanel.Domain.Models;
using BankingControlPanel.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BankingControlPanel.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<SearchCriteria> SearchCriterias { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").IsRequired();
            });
            entity.OwnsOne(c => c.PersonalId, personalId =>
            {
                personalId.Property(p => p.Value).HasColumnName("PersonalId").IsRequired();
            });

            entity.HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}