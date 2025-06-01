using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class TransactionHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; }

        [ForeignKey(nameof(PerformedByUser))]
        public int PerformedByUserID { get; set; }

        public DateTime ActionTime { get; set; } = DateTime.Now;

        public string Notes { get; set; }

        public Contract Contract { get; set; }
        public UserAccount PerformedByUser { get; set; }
    }

}
