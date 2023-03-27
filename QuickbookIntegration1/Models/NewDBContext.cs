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
        public virtual DbSet<_Bill> Bills { get; set; } = null!;
        public virtual DbSet<_Item> Items { get; set; } = null!;
        public virtual DbSet<Po> Pos { get; set; } = null!;
        public virtual DbSet<vendor> Vendors { get; set; } = null!;
        public virtual DbSet<customer> cust { get; set; } = null!;
        public virtual DbSet<invoice> inv { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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

            modelBuilder.Entity<_Bill>(entity =>
            {
                entity.ToTable("bill");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.VendorRef);
                entity.Property(e => e.apaccountRef);
                entity.Property(e => e.due);
                entity.Property(e => e.itemref);
                entity.Property(e => e.Qty);
                entity.Property(e => e.Billno);
                entity.Property(e => e.rate);
               /* entity.Property(e => e.vendorid);*/
                entity.Property(e => e.QBbillid).HasDefaultValue(null);
            });

            modelBuilder.Entity<_Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.expenseaccref);

                entity.Property(e => e.QBitem).HasDefaultValue(null);
                entity.Property(e => e.SKU);
                entity.Property(e => e.cost);
            });

            modelBuilder.Entity<Po>(entity =>
            {
                entity.ToTable("po");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.VendorRef);
                entity.Property(e => e.itemid);
                entity.Property(e => e.itemaccref);
                entity.Property(e => e.due);
                entity.Property(e => e.qty);
                entity.Property(e => e.rate);
                /*entity.Property(e => e.vendorid);*/
                entity.Property(e => e.QBid).HasDefaultValue(null);
                /*entity.Property(e => e.Line0N)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Line [0..n]");
                entity.Property(e => e.VendorRef);*/

               /* entity.HasOne(d => d.VendorRefNavigation)
                    .WithMany(p => p.Pos)
                    .HasForeignKey(d => d.VendorRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PO_Vendor")*/
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
            modelBuilder.Entity<customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.DisplayName);
                entity.Property(e => e.Email);
                entity.Property(e => e.Mobile);
                entity.Property(e => e.QBcustid).HasDefaultValue(null);
            });
            modelBuilder.Entity<invoice>(entity =>
            {
                entity.ToTable("Invoice");
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.customerref);
                entity.Property(e => e.itemref);
                entity.Property(e => e.Email);
                entity.Property(e => e.qty);
                entity.Property(e => e.rate);
                entity.Property(e=>e.QBInvid).HasDefaultValue(null);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
