using Microsoft.EntityFrameworkCore;
using PracticalProject.AppContext.DBModels;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PracticalProject.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductMaster> Products { get; set; }
        public DbSet<InvoiceMaster> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            modelBuilder.Entity<InvoiceDetail>()
       .HasOne(b => b.invoice)
       .WithMany(a => a.invoiceDetails)
       .HasForeignKey(b => b.InvoiceId)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceDetail>()
        .HasOne(b => b.Product)
        .WithMany(a => a.invoiceDetails)
        .HasForeignKey(b => b.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

        

        }
    }

    public static class ModalBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaster>().HasData(
                   new ProductMaster
                   {
                       ProductId = Guid.NewGuid(),
                       ProductName = "Samsung-battery",
                       
                   }, new ProductMaster
                   {
                       ProductId = Guid.NewGuid(),
                       ProductName = "Nokia-battery"
                   });
        }
    }
}
