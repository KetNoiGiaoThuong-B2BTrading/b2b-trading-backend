using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class QuotationResponse
    {
        [Key]
        public int ResponseID { get; set; }

        [ForeignKey(nameof(QuotationRequest))]
        public int RequestID { get; set; }

        [ForeignKey(nameof(BuyerCompany))]
        public int BuyerCompanyID { get; set; }

        [ForeignKey(nameof(SellerCompany))]
        public int SellerCompanyID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProposedPrice { get; set; }

        [MaxLength(100)]
        public string DeliveryTime { get; set; }

        [MaxLength(100)]
        public string ShippingMethod { get; set; }

        public string Terms { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public QuotationRequest QuotationRequest { get; set; }
        public Company BuyerCompany { get; set; }
        public Company SellerCompany { get; set; }
    }
}
