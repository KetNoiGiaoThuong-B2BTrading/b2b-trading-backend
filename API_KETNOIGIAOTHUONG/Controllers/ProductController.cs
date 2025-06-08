// Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using API_KETNOIGIAOTHUONG.DTOs.Product;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly KNGTContext _context;

        public ProductController(KNGTContext context)
        {
            _context = context;
        }

        // API tạo sản phẩm mới
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                CompanyID = dto.CompanyID,
                CategoryID = dto.CategoryID,
                ProductName = dto.ProductName,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
                Image = dto.Image,
                Status = "Available",
                CreatedDate = DateTime.Now
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tạo sản phẩm thành công", productId = product.ProductID });
        }

        // API lấy thông tin sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductResponseDTO
                {
                    ProductID = (int)p.ProductID,
                    CompanyID = (int)p.CompanyID,
                    CategoryID = (int)p.CategoryID,
                    ProductName = p.ProductName == null ? "" : p.ProductName,
                    Description = p.Description == null ? "" : p.Description,
                    UnitPrice = (int)p.UnitPrice,
                    StockQuantity = (int)p.StockQuantity,
                    Status = p.Status == null ? "" : p.Status,
                    Image = p.Image == null ? "" : p.Image,
                    CreatedDate = (DateTime)p.CreatedDate,
                    ApprovedBy = p.ApprovedBy,
                    ApprovalNotes = p.ApprovalNotes == null ? "" : p.ApprovalNotes,
                    CompanyName = p.Company.CompanyName == null ? "" : p.Company.CompanyName,
                    CategoryName = p.Category.CategoryName == null ? "" : p.Category.CategoryName
                })
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return Ok(new ProductResponseDTO());

            return Ok(product);
        }

        // API cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return Ok(new { message = "Không tìm thấy sản phẩm" });

            product.ProductName = dto.ProductName ?? "";
            product.Description = dto.Description ?? "";
            product.UnitPrice = dto.UnitPrice;
            product.StockQuantity = dto.StockQuantity;
            product.Image = dto.Image ?? "";
            product.Status = dto.Status ?? "Available";

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật sản phẩm thành công" });
        }

        // API xoá sản phẩm
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return Ok(new { message = "Không tìm thấy sản phẩm" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xoá sản phẩm thành công" });
        }

        // API danh sách sản phẩm theo công ty
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetProductsByCompany(int companyId)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => p.CompanyID == companyId)
                .Select(p => new ProductResponseDTO
                {
                    ProductID = (int)p.ProductID,
                    CompanyID = (int)p.CompanyID,
                    CategoryID = (int)p.CategoryID,
                    ProductName = p.ProductName == null ? "" : p.ProductName,
                    Description = p.Description == null ? "" : p.Description,
                    UnitPrice = (int)p.UnitPrice,
                    StockQuantity = (int)p.StockQuantity,
                    Status = p.Status == null ? "" : p.Status,
                    Image = p.Image == null ? "" : p.Image,
                    CreatedDate = (DateTime)p.CreatedDate,
                    ApprovedBy = p.ApprovedBy,
                    ApprovalNotes = p.ApprovalNotes == null ? "" : p.ApprovalNotes,
                    CompanyName = p.Company.CompanyName == null ? "" : p.Company.CompanyName,
                    CategoryName = p.Category.CategoryName == null ? "" : p.Category.CategoryName
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}