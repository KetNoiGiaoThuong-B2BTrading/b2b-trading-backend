using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }  // Consider Enum for role

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public Company Company { get; set; }

      
    }

}
