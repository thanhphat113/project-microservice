using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace IdentityService.Models;

public partial class IdentityDbContext : DbContext
{
    public IdentityDbContext()
    {
    }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "IX_Account_UserId");

            entity.HasIndex(e => e.AccountId, "UQ__Account__349DA5A7AEAE707B").IsUnique();

            entity.Property(e => e.Identify).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Provider).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Account__UserId__3B75D760");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.UserId, "IX_RefreshToken_UserId");

            entity.HasIndex(e => e.TokenId, "UQ__tmp_ms_x__658FEEEB8DC1D79C").IsUnique();

            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__5EBF139D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "UQ__User__1788CC4D882CE5A2").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
