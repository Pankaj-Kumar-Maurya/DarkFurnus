using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DarkFurnus.Models;

public partial class DarkFurnusDbContext : DbContext
{
    public DarkFurnusDbContext()
    {
    }

    public DarkFurnusDbContext(DbContextOptions<DarkFurnusDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblUserRegistration> TblUserRegistrations { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tblCount__D32076BCAFA12F3B");

            entity.ToTable("tblCountry");

            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("countryName");
        });

        modelBuilder.Entity<TblUserRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUserR__3213E83FA4535897");

            entity.ToTable("tblUserRegistration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
