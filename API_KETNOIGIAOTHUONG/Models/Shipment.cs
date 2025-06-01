using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        [Required, MaxLength(100)]
        public string Status { get; set; }

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public Contract Contract { get; set; }
    }

}
