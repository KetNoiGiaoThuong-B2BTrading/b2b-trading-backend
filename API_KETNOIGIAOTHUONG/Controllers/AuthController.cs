// Controllers/AuthController.cs
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.DTOs;
using API_KETNOIGIAOTHUONG.DTOs.User;
using API_KETNOIGIAOTHUONG.Models;
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


    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDTO dto)
    {
        // Kiểm tra email đã tồn tại
        var existingUser = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null)
            return BadRequest("Email này đã được sử dụng.");

        // ✅ Kiểm tra Role hợp lệ
        var validRoles = new[] { "Admin", "Company", "User" };
        if (!validRoles.Contains(dto.Role))
            return BadRequest("Role không hợp lệ. Các giá trị hợp lệ: Admin, Company, User.");

        // Tạo công ty mới
        var company = new Company
        {
            CompanyName = dto.CompanyName,
            TaxCode = dto.TaxCode,
            BusinessSector = dto.BusinessSector,
            Address = dto.Address,
            Representative = dto.Representative,
            Email = dto.CompanyEmail,
            PhoneNumber = dto.PhoneNumber,
            LegalDocuments = dto.LegalDocuments,
            ImageCompany = dto.ImageCompany,
            VerificationStatus = "Pending"
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync(); // Lưu để có CompanyID

        // Tạo tài khoản người dùng gắn với công ty
        var user = new UserAccount
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Password = PasswordHelper.HashPassword(dto.Password), // ✅ Mã hoá mật khẩu
            Role = "User",
            CompanyID = company.CompanyID,
            Status = "Active"
        };

        _context.UserAccounts.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Đăng ký thành công!", user.UserID });
    }

}


