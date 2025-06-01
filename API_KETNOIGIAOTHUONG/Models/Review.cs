using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey(nameof(SenderCompany))]
        public int SenderCompanyID { get; set; }

        [ForeignKey(nameof(ReceiverCompany))]
        public int ReceiverCompanyID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public Company SenderCompany { get; set; }
        public Company ReceiverCompany { get; set; }
        public Contract Contract { get; set; }
    }


}
