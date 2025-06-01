using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class CompanyDocument
    {
        [Key]
        public int DocumentID { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }

        [Required, MaxLength(50)]
        public string DocumentType { get; set; }

        [Required, MaxLength(500)]
        public string FilePath { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public Company Company { get; set; }
    }
}
