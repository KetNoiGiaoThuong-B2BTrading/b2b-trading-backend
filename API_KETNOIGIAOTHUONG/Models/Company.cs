using System.ComponentModel.DataAnnotations;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        [Required, MaxLength(200)]
        public string CompanyName { get; set; }

        [Required, MaxLength(50)]
        public string TaxCode { get; set; }

        [MaxLength(200)]
        public string BusinessSector { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Representative { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string VerificationStatus { get; set; }

        public string LegalDocuments { get; set; }

        public string ImageCompany { get; set; }

        // Navigation Properties

    }

}
