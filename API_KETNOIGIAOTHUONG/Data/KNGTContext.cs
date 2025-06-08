using API_KETNOIGIAOTHUONG.Models;
using Microsoft.EntityFrameworkCore;

namespace API_KETNOIGIAOTHUONG.Data
{
    public class KNGTContext : DbContext
    {
        public KNGTContext(DbContextOptions<KNGTContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyDocument> CompanyDocuments { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<QuotationRequest> QuotationRequests { get; set; }
        public DbSet<QuotationResponse> QuotationResponses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<InvestmentRound> InvestmentRounds { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PeriodicTransaction> PeriodicTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Company>().ToTable("Company", "dbo");
            //modelBuilder.Entity<UserAccount>().ToTable("UserAccount","dbo"); // ⚠️ Không "s"
            //modelBuilder.Entity<Category>().ToTable("Category", "dbo"); // ⚠️ Không "s"
            modelBuilder.Entity<Company>().ToTable("Company", "dbo");
            modelBuilder.Entity<CompanyDocument>().ToTable("CompanyDocument", "dbo");
            modelBuilder.Entity<UserAccount>().ToTable("UserAccount", "dbo");
            modelBuilder.Entity<Category>().ToTable("Category", "dbo");
            modelBuilder.Entity<Product>().ToTable("Product", "dbo");
            modelBuilder.Entity<QuotationRequest>().ToTable("QuotationRequest", "dbo");
            modelBuilder.Entity<QuotationResponse>().ToTable("QuotationResponse", "dbo");
            modelBuilder.Entity<Contract>().ToTable("Contract", "dbo");
            modelBuilder.Entity<Payment>().ToTable("Payment", "dbo");
            modelBuilder.Entity<Shipment>().ToTable("Shipment", "dbo");
            modelBuilder.Entity<Review>().ToTable("Review", "dbo");
            modelBuilder.Entity<TransactionHistory>().ToTable("TransactionHistory", "dbo");
            modelBuilder.Entity<InvestmentRound>().ToTable("InvestmentRound", "dbo");
            modelBuilder.Entity<Notification>().ToTable("Notification", "dbo");
            modelBuilder.Entity<PeriodicTransaction>().ToTable("PeriodicTransaction", "dbo");

        }

    }
}
