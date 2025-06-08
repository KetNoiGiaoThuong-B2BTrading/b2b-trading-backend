namespace API_KETNOIGIAOTHUONG.DTOs.User
{
    public class UserRegisterDTO
    {
        // Thông tin công ty
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string BusinessSector { get; set; }
        public string Address { get; set; }
        public string Representative { get; set; }
        public string CompanyEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string LegalDocuments { get; set; }
        public string ImageCompany { get; set; }

        // Thông tin tài khoản
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Company"; // mặc định là tài khoản doanh nghiệp
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponseDTO
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
    }
}
