namespace API_KETNOIGIAOTHUONG.DTOs.User
{
    public class RegisterUserDTO
    {
        public int CompanyID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
