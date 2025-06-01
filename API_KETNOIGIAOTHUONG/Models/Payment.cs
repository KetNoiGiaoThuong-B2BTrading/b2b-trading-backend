using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required, MaxLength(100)]
        public string Method { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public string Notes { get; set; }

        public Contract Contract { get; set; }
    }
}
