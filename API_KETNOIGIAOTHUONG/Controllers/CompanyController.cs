using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.DTOs.Company;
using API_KETNOIGIAOTHUONG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly KNGTContext _context;

        public CompanyController(KNGTContext context)
        {
            _context = context;
        }

        // Lấy tất cả công ty
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _context.Companies
                .Select(c => new CompanyResponseDTO
                {
                    CompanyID = c.CompanyID,
                    CompanyName = c.CompanyName,
                    TaxCode = c.TaxCode,
                    BusinessSector = c.BusinessSector,
                    Address = c.Address,
                    Representative = c.Representative,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    VerificationStatus = c.VerificationStatus,
                    ImageCompany = c.ImageCompany,
                })
                .ToListAsync();

            return Ok(companies);
        }


        // Tạo công ty mới
        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = new Company
            {
                CompanyName = dto.CompanyName,
                TaxCode = dto.TaxCode,
                BusinessSector = dto.BusinessSector,
                Address = dto.Address,
                Representative = dto.Representative,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                LegalDocuments = dto.LegalDocuments,
                VerificationStatus = "Chưa xác minh",
                ImageCompany = dto.ImageCompany,
            };

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tạo công ty thành công", companyId = company.CompanyID });
        }

        // Lấy thông tin công ty theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
                return NotFound($"Không tìm thấy công ty với id = {id}");

            var response = new CompanyResponseDTO
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                TaxCode = company.TaxCode,
                BusinessSector = company.BusinessSector,
                Address = company.Address,
                Representative = company.Representative,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                VerificationStatus = company.VerificationStatus,
                ImageCompany = company.ImageCompany

            };

            return Ok(response);
        }

        // Cập nhật công ty
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CreateCompanyDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound($"Không tìm thấy công ty với id = {id}");

            company.CompanyName = dto.CompanyName;
            company.TaxCode = dto.TaxCode;
            company.BusinessSector = dto.BusinessSector;
            company.Address = dto.Address;
            company.Representative = dto.Representative;
            company.Email = dto.Email;
            company.PhoneNumber = dto.PhoneNumber;
            company.LegalDocuments = dto.LegalDocuments;
            company.VerificationStatus = dto.VerificationStatus;
            company.ImageCompany = dto.ImageCompany;

            _context.Companies.Update(company);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật công ty thành công" });
        }

        // Duyệt hồ sơ công ty
        [HttpPost("verify/{id}")]
        public async Task<IActionResult> VerifyCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound($"Không tìm thấy công ty với id = {id}");

            company.VerificationStatus = "Đã xác minh";
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Công ty đã được xác minh" });
        }

        // Lấy danh sách công ty chờ duyệt
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingCompanies()
        {
            var pending = await _context.Companies
                .Where(c => c.VerificationStatus == "Chưa xác minh")
                .Select(c => new CompanyResponseDTO
                {
                    CompanyID = c.CompanyID,
                    CompanyName = c.CompanyName,
                    TaxCode = c.TaxCode,
                    BusinessSector = c.BusinessSector,
                    Address = c.Address,
                    Representative = c.Representative,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    VerificationStatus = c.VerificationStatus,
                    ImageCompany = c.ImageCompany,
                })
                .ToListAsync();

            return Ok(pending);
        }
    }
}
