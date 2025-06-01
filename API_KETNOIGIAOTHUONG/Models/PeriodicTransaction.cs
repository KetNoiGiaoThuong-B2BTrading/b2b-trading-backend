using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class PeriodicTransaction
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        public DateTime DueDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public string InvoiceFile { get; set; }

        public string ReportFile { get; set; }

        public Contract Contract { get; set; }
    }

}
