namespace API_KETNOIGIAOTHUONG.DTOs
{
    public class Auth
    {
    }

    // Models/Auth/LoginRequest.cs
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Models/Auth/ChangePasswordRequest.cs
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
