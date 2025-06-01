using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class QuotationRequest
    {
        [Key]
        public int RequestID { get; set; }

        [ForeignKey(nameof(BuyerCompany))]
        public int BuyerCompanyID { get; set; }

        [ForeignKey(nameof(SellerCompany))]
        public int SellerCompanyID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string DeliveryTime { get; set; }

        public string AdditionalRequest { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public Company BuyerCompany { get; set; }
        public Company SellerCompany { get; set; }
        public Product Product { get; set; }

  
    }

}
