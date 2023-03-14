using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuickbookIntegration1.Models
{
    public partial class FinalDBContext : DbContext
    {
        public FinalDBContext()
        {
        }

        public FinalDBContext(DbContextOptions<FinalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VendorList> VendorLists { get; set; } = null!;
        public virtual DbSet<purchaseorder> purchases { get; set; } = null!;
        public virtual DbSet<accountlist> AccountLists { get; set; }= null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FinalDB; Trusted_Connection=true;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorList>(entity =>
            {
                entity.ToTable("Vendor_List");

                entity.Property(e => e.BillAddr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyRef)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gstin)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GSTIN");

                entity.Property(e => e.PrimaryEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
