using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }

        [ForeignKey(nameof(SellerCompany))]
        public int SellerCompanyID { get; set; }

        [ForeignKey(nameof(BuyerCompany))]
        public int BuyerCompanyID { get; set; }

        [Required, MaxLength(50)]
        public string ContractType { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Terms { get; set; }

        public string DisputeResolution { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public string SellerSignature { get; set; }

        public string BuyerSignature { get; set; }

        public DateTime? SignDate { get; set; }

        public string ContractFile { get; set; }

        public bool DigitalSignature { get; set; } = false;

        public Company SellerCompany { get; set; }
        public Company BuyerCompany { get; set; }

      
    }

}
