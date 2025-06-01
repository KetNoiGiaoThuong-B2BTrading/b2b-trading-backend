// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using API_KETNOIGIAOTHUONG.DTOs.User;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // API đăng ký người dùng mới
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO dto)
        {
            return Ok("Đăng ký người dùng thành công");
        }

        // API đăng nhập
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            return Ok("Đăng nhập thành công");
        }

        // API lấy thông tin người dùng
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok();
        }

        // API danh sách người dùng theo công ty
        [HttpGet("company/{companyId}")]
        public IActionResult GetUsersByCompany(int companyId)
        {
            return Ok();
        }
    }
}