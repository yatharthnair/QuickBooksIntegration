using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuickbookIntegration1.Models
{
    public partial class QuickBooksContext : DbContext
    {
        public QuickBooksContext()
        {
        }

        public QuickBooksContext(DbContextOptions<QuickBooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PurchaseOrderList> PurchaseOrderLists { get; set; } = null!;
        public virtual DbSet<SalesOrderList> SalesOrderLists { get; set; } = null!;
        public virtual DbSet<VendorList> VendorLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=QuickBooks; Trusted_Connection=true;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrderList>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderId);

                entity.ToTable("Purchase_Order_List");

                entity.Property(e => e.PurchaseOrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("PurchaseOrderID");

                entity.Property(e => e.DateOfDelivery).HasColumnType("date");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.PriceItem).HasColumnName("Price/Item");
            });

            modelBuilder.Entity<SalesOrderList>(entity =>
            {
                entity.HasKey(e => e.SalesOrderId);

                entity.ToTable("Sales_Order_List");

                entity.Property(e => e.SalesOrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("SalesOrderID");

                entity.Property(e => e.DateOfOrder).HasColumnType("date");

                entity.Property(e => e.PriceItem).HasColumnName("Price/Item");
            });

            modelBuilder.Entity<VendorList>(entity =>
            {
                entity.ToTable("Vendor_List");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gstno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GSTNo");

                entity.Property(e => e.PanId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PanID");

                entity.Property(e => e.PrimaryEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Suffix)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorBusiness)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
