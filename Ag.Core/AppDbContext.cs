using Ag.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Ag.DAL;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__CARD__CF0FBF26288F65C6");

            entity.ToTable("CARD");

            entity.HasIndex(e => new { e.CardId, e.ClientId }, "UQ_CARD_CLIENT").IsUnique();

            entity.HasIndex(e => e.CardNumber, "UQ__CARD__9911325D45F6ED13").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("CARD_ID");
            entity.Property(e => e.Bank)
                .HasMaxLength(100)
                .HasColumnName("BANK");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.CardType).HasColumnName("CARD_TYPE");
            entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");
            entity.Property(e => e.ExpiryMonth).HasColumnName("EXPIRY_MONTH");
            entity.Property(e => e.ExpiryYear).HasColumnName("EXPIRY_YEAR");

            entity.HasOne(d => d.Client).WithMany(p => p.Cards)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CARD__CLIENT_ID__286302EC");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__CLIENT__AA1D43785BF059BE");

            entity.ToTable("CLIENT");

            entity.Property(e => e.Code).HasColumnName("CODE");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .HasColumnName("CONTACT_NUMBER");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Occupation)
                .HasMaxLength(100)
                .HasColumnName("OCCUPATION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
