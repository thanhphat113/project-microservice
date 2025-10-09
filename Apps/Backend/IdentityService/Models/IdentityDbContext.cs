using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A6BAD6BD3F");

            entity.ToTable("Account");

            entity.HasIndex(e => e.AccountId, "UQ__Account__349DA5A7AEAE707B").IsUnique();

            entity.Property(e => e.Identify)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Account__UserId__3B75D760");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__tmp_ms_x__658FEEEABAD99E56");

            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.TokenId, "UQ__tmp_ms_x__658FEEEB8DC1D79C").IsUnique();

            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__5EBF139D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CC3195CA0");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "UQ__User__1788CC4D882CE5A2").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasData(new User
            {
                UserId = 1,
                Name = "Demo",
                Email = "demo@gmail.com"
            });
        });

        modelBuilder.Entity<Account>().HasData(new Account
        {
            AccountId = 1,
            Identify = "demo@gmail.com",
            PasswordHash = "AQAAAAIAAYagAAAAEJnHg+CIGqb8SRQcT9MpZbU11FyRYuyxzTJkDEHG9mG6rkmAaDkODwYOCVbiW+kxyw==",
            Provider = "Local",
            UserId = 1
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
