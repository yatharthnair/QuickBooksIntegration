using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuickbookIntegration1.Models
{
    public partial class NewDBContext : DbContext
    {
        public NewDBContext()
        {
        }

        public NewDBContext(DbContextOptions<NewDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<_Item> Items { get; set; } = null!;
        public virtual DbSet<Po> Pos { get; set; } = null!;
        public virtual DbSet<vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=NewDB; Trusted_Connection=true;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Account");

                entity.ToTable("account");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.AcctNum)
                    .HasMaxLength(50);

                /*  entity.Property(e => e.AccountSubType)
                      .HasMaxLength(50)
                      .IsUnicode(false);*/

                entity.Property(e => e.CurrentBalance);
                entity.Property(e => e.QBaccid).HasDefaultValue(null);
                /* entity.Property(e => e.AccountType)
                     .HasMaxLength(50)
                     .IsUnicode(false);*/

                /*entity.Property(e => e.SyncToken).ValueGeneratedOnAdd();*/
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrencyRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line0N)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Line [0..n]");

                entity.Property(e => e.SyncToken).ValueGeneratedOnAdd();

                entity.HasOne(d => d.VendorRefNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.VendorRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Vendor");
            });

            modelBuilder.Entity<_Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QBitem).HasDefaultValue(null);
            });

            modelBuilder.Entity<Po>(entity =>
            {
                entity.ToTable("po");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApaccountRef).HasColumnName("APAccountRef");

                entity.Property(e => e.CurrencyRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line0N)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Line [0..n]");

                entity.HasOne(d => d.VendorRefNavigation)
                    .WithMany(p => p.Pos)
                    .HasForeignKey(d => d.VendorRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PO_Vendor");
            });

            modelBuilder.Entity<vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.OtherContactInfo);

                entity.Property(e => e.Gstin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GSTIN");

                entity.Property(e => e.PrimaryEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.QBid).HasDefaultValue(null);
                /* entity.Property(e => e.SyncToken).ValueGeneratedOnAdd();*/
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
