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

            modelBuilder.Entity<Company>().ToTable("Company", "dbo");

            //// Danh mục cha - con (Category)
            //modelBuilder.Entity<Category>()
            //    .HasOne(c => c.ParentCategory)
            //    .WithMany(c => c.SubCategories)
            //    .HasForeignKey(c => c.ParentCategoryID)
            //    .OnDelete(DeleteBehavior.Restrict); // tránh xóa cascade đệ quy

            //// Công ty - Tài liệu (Company - CompanyDocument)
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.CompanyDocuments)
            //    .WithOne(d => d.Company)
            //    .HasForeignKey(d => d.CompanyID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Công ty - Người dùng (Company - UserAccount)
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.UserAccounts)
            //    .WithOne(u => u.Company)
            //    .HasForeignKey(u => u.CompanyID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Công ty - Sản phẩm (Company - Product)
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.Products)
            //    .WithOne(p => p.Company)
            //    .HasForeignKey(p => p.CompanyID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Sản phẩm - Danh mục (Product - Category)
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Category)
            //    .WithMany(c => c.Products)
            //    .HasForeignKey(p => p.CategoryID)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////// Báo giá yêu cầu - Người tạo (QuotationRequest - UserAccount)
            ////modelBuilder.Entity<QuotationRequest>()
            ////    .HasOne(q => q.Requester)
            ////    .WithMany(u => u.QuotationRequests)
            ////    .HasForeignKey(q => q.RequesterID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            //// Báo giá phản hồi - Báo giá yêu cầu (QuotationResponse - QuotationRequest)
            //modelBuilder.Entity<QuotationResponse>()
            //    .HasOne(qr => qr.QuotationRequest)
            //    .WithMany(q => q.QuotationResponses)
            //    .HasForeignKey(qr => qr.ResponseID)
            //    .OnDelete(DeleteBehavior.Cascade);

            ////// Hợp đồng - Báo giá phản hồi (Contract - QuotationResponse)
            ////modelBuilder.Entity<Contract>()
            ////    .HasOne(c => c.QuotationResponse)
            ////    .WithMany(qr => qr.Contracts)
            ////    .HasForeignKey(c => c.QuotationResponseID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            //// Thanh toán - Hợp đồng (Payment - Contract)
            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Contract)
            //    .WithMany(c => c.Payments)
            //    .HasForeignKey(p => p.ContractID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Vận chuyển - Hợp đồng (Shipment - Contract)
            //modelBuilder.Entity<Shipment>()
            //    .HasOne(s => s.Contract)
            //    .WithMany(c => c.Shipments)
            //    .HasForeignKey(s => s.ContractID)
            //    .OnDelete(DeleteBehavior.Cascade);

            ////// Đánh giá - Người dùng (Review - UserAccount)
            ////modelBuilder.Entity<Review>()
            ////    .HasOne(r => r.UserAccount)
            ////    .WithMany(u => u.Reviews)
            ////    .HasForeignKey(r => r.UserAccountID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            ////// Đánh giá - Sản phẩm (Review - Product)
            ////modelBuilder.Entity<Review>()
            ////    .HasOne(r => r.Product)
            ////    .WithMany(p => p.Reviews)
            ////    .HasForeignKey(r => r.ProductID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            ////// Lịch sử giao dịch - Người dùng (TransactionHistory - UserAccount)
            ////modelBuilder.Entity<TransactionHistory>()
            ////    .HasOne(t => t.UserAccount)
            ////    .WithMany(u => u.TransactionHistories)
            ////    .HasForeignKey(t => t.UserAccountID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            ////// Đợt gọi vốn - Công ty (InvestmentRound - Company)
            ////modelBuilder.Entity<InvestmentRound>()
            ////    .HasOne(i => i.Company)
            ////    .WithMany(c => c.InvestmentRounds)
            ////    .HasForeignKey(i => i.CompanyID)
            ////    .OnDelete(DeleteBehavior.Cascade);

            //// Thông báo - Người dùng (Notification - UserAccount)
            //modelBuilder.Entity<Notification>()
            //    .HasOne(n => n.UserAccount)
            //    .WithMany(u => u.Notifications)
            //    .HasForeignKey(n => n.UserID)
            //    .OnDelete(DeleteBehavior.Cascade);

            ////// Giao dịch định kỳ - Người dùng (PeriodicTransaction - UserAccount)
            ////modelBuilder.Entity<PeriodicTransaction>()
            ////    .HasOne(p => p.UserAccount)
            ////    .WithMany(u => u.PeriodicTransactions)
            ////    .HasForeignKey(p => p.UserAccountID)
            ////    .OnDelete(DeleteBehavior.Cascade);
            //// Company - BuyerContracts (1 - N)
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.BuyerContracts)
            //    .WithOne(ct => ct.BuyerCompany) // navigation ngược bên Contract
            //    .HasForeignKey(ct => ct.BuyerCompanyID)
            //    .OnDelete(DeleteBehavior.Restrict); // hoặc Cascade tùy yêu cầu

            //// Nếu Company còn có SellerContracts (giả sử), cấu hình tương tự
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.SellerContracts)
            //    .WithOne(ct => ct.SellerCompany)
            //    .HasForeignKey(ct => ct.SellerCompanyID)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
