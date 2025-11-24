using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UmetnickaDelaProjekat1.Models;

public partial class UmetnickaDelaContext : DbContext
{
    public UmetnickaDelaContext()
    {
    }

    public  UmetnickaDelaContext(DbContextOptions<UmetnickaDelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Korisnici> Korisnici { get; set; }

    public virtual DbSet<Porudzbine> Porudzbine { get; set; }

    public virtual DbSet<StavkePorudzbine> StavkePorudzbine { get; set; }

    public virtual DbSet<UmetnickaDela> UmetnickaDela { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Andrija;Database=Umetnicka_Dela;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Korisnic__3214EC277701AEFA");

            entity.ToTable("Korisnici");

            entity.HasIndex(e => e.Email, "UQ__Korisnic__A9D10534B82EEBC0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.LozinkaHash).HasMaxLength(255);
            entity.Property(e => e.Prezime).HasMaxLength(50);
            entity.Property(e => e.TipKorisnika).HasMaxLength(20);
        });

        modelBuilder.Entity<Porudzbine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Porudzbi__3214EC274CCC56BC");

            entity.ToTable("Porudzbine");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DatumPorudzbine)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Porudzbines)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Porudzbin__Koris__403A8C7D");
        });

        modelBuilder.Entity<StavkePorudzbine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StavkePo__3214EC274FC2C2F9");

            entity.ToTable("StavkePorudzbine");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CenaPoJedinici).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PorudzbinaId).HasColumnName("PorudzbinaID");
            entity.Property(e => e.UmetnickoDeloId).HasColumnName("UmetnickoDeloID");

            entity.HasOne(d => d.Porudzbina).WithMany(p => p.StavkePorudzbines)
                .HasForeignKey(d => d.PorudzbinaId)
                .HasConstraintName("FK__StavkePor__Porud__440B1D61");

            entity.HasOne(d => d.UmetnickoDelo).WithMany(p => p.StavkePorudzbines)
                .HasForeignKey(d => d.UmetnickoDeloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StavkePor__Umetn__44FF419A");
        });

        modelBuilder.Entity<UmetnickaDela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Umetnick__3214EC272C41F8E0");

            entity.ToTable("UmetnickaDela");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Dostupnost).HasDefaultValue(true);
            entity.Property(e => e.Fotografija).HasMaxLength(255);
            entity.Property(e => e.Kategorija).HasMaxLength(100);
            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Umetnik).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
