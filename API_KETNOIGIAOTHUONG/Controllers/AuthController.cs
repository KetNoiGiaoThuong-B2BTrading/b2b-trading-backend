// Controllers/AuthController.cs
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;
using API_KETNOIGIAOTHUONG.DTOs;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly KNGTContext _context;

    public AuthController(KNGTContext context)
    {
        _context = context;
    }

    // 🟢 Đăng nhập
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] API_KETNOIGIAOTHUONG.DTOs.LoginRequest request)

    {
        var hashedPassword = PasswordHelper.HashPassword(request.Password);

        var user = await _context.UserAccounts
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == hashedPassword && u.Status == "Active");

        if (user == null)
            return Unauthorized("Sai email hoặc mật khẩu");

        return Ok(new
        {
            user.UserID,
            user.FullName,
            
            user.Email,
            user.Role,
            user.CompanyID
        });
    }

    // 🟡 Đổi mật khẩu
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = await _context.UserAccounts
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.Status == "Active");

        if (user == null)
            return NotFound("Không tìm thấy người dùng");

        var currentHashed = PasswordHelper.HashPassword(request.CurrentPassword);
        if (user.Password != currentHashed)
            return BadRequest("Mật khẩu hiện tại không đúng");

        user.Password = PasswordHelper.HashPassword(request.NewPassword);
        _context.UserAccounts.Update(user);
        await _context.SaveChangesAsync();

        return Ok("Đổi mật khẩu thành công");
    }
}


