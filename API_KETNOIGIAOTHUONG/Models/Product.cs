using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }

        [Required, MaxLength(200)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        [MaxLength(50)]
        public string Status { get; set; } = "Available";

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int? ApprovedBy { get; set; }

        public string ApprovalNotes { get; set; }

        public Company Company { get; set; }
        public Category Category { get; set; }
        public UserAccount ApprovedByUser { get; set; }

       
    }

}
