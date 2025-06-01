using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class InvestmentRound
    {
        [Key]
        public int RoundID { get; set; }

        [ForeignKey(nameof(Contract))]
        public int ContractID { get; set; }

        [Required, MaxLength(200)]
        public string ProjectName { get; set; }

        [Required, MaxLength(200)]
        public string StageName { get; set; }

        public float InvestmentRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvestmentAmount { get; set; }

        public float ProfitCommitment { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime PlannedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        [Range(0, 100)]
        public float Progress { get; set; } = 0;

        [Required, MaxLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualRevenue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualProfit { get; set; }

        public string Notes { get; set; }

        public Contract Contract { get; set; }
    }

}
