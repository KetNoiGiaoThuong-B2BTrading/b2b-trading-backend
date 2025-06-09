// Controllers/ProductController.cs
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.DTOs.Product;
using API_KETNOIGIAOTHUONG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/Product
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Company)

                .ToListAsync();

            var result = products.Select(p => new ProductResponseDTO
            {

                ProductID = (int)p.ProductID,
                CompanyID= (int)p.CompanyID,
                CategoryID = (int)p.CategoryID,
                ProductName = p.ProductName,

              

                Description = p.Description,
                UnitPrice = (double)p.UnitPrice,  // ép kiểu decimal -> double
                StockQuantity = (int)p.StockQuantity,
                Status = p.Status,
                Image = p.Image,

                CreatedDate = (DateTime)p.CreatedDate,


            }).ToList();

            return Ok(result);
        }

        // GET: api/Product
        [HttpGet("get-by-company-id/{company_id}")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductsByCompanyId(int company_id)
        {
            var products = await _context.Products
                .Include(p => p.Company)
                .Where(p => p.CompanyID == company_id)
                .ToListAsync();

            if (products == null || products.Count == 0)
                return NotFound();

            var result = products.Select(p => new ProductResponseDTO
            {
                ProductID = (int)p.ProductID,
                ProductName = p.ProductName,
                Description = p.Description,
                UnitPrice = (double)p.UnitPrice,
                StockQuantity = (int)p.StockQuantity,
                Status = p.Status,
                Image = p.Image,
                CategoryID = (int)p.CategoryID,
                CompanyID = (int)p.CompanyID,
                CreatedDate = (DateTime)p.CreatedDate
            });

            return Ok(result);
        }


        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProductById(int id)
        {
            var p = await _context.Products
                .Include(p => p.Company)

                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (p == null)
                return NotFound();

            var result = new ProductResponseDTO
            {
                ProductID = (int)p.ProductID,
                ProductName = p.ProductName,
                Description = p.Description,
                UnitPrice = (double)p.UnitPrice,
                StockQuantity = (int)p.StockQuantity,
                Status = p.Status,
                Image = p.Image,
                CreatedDate = (DateTime)p.CreatedDate
            };

            return Ok(result);
        }

        ////POST: api/Product
        //[HttpPost("create")]
        //public async Task<ActionResult<Product>> CreateProduct(Product product)
        //{
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, product);
        //}

        //// PUT: api/Product/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, Product product)
        //{
        //    if (id != product.ProductID)
        //        return BadRequest("ID không khớp");

        //    var existingProduct = await _context.Products.FindAsync(id);
        //    if (existingProduct == null)
        //        return NotFound();

        //    existingProduct.ProductName = product.ProductName;
        //    existingProduct.Description = product.Description;
        //    existingProduct.UnitPrice = product.UnitPrice;
        //    existingProduct.StockQuantity = product.StockQuantity;
        //    existingProduct.Status = product.Status;
        //    existingProduct.Image = product.Image;


        //    existingProduct.CreatedDate = product.CreatedDate;

        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        //// DELETE: api/Product/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //        return NotFound();

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        // POST: api/Product
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            // ✅ Kiểm tra Company tồn tại
            var company = await _context.Companies.FindAsync(dto.CompanyID);
            if (company == null)
                return NotFound("Không tìm thấy công ty với ID đã cho.");

            // ✅ Kiểm tra Category tồn tại
            var category = await _context.Categories.FindAsync(dto.CategoryID);
            if (category == null)
                return NotFound("Không tìm thấy danh mục với ID đã cho.");

            // ✅ Tạo mới sản phẩm
            var product = new Product
            {
                CompanyID = dto.CompanyID,
                CategoryID = dto.CategoryID,
                ProductName = dto.ProductName,
                Description = dto.Description,
                UnitPrice = (decimal?)dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
                Image = dto.Image,
                Status = dto.Status ?? "Available",
                CreatedDate = dto.CreatedDate
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Tạo sản phẩm thành công!",
                product.ProductID
            });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm với ID đã cho.");

            // Gán toàn bộ giá trị từ DTO vào đối tượng sản phẩm
            product.CompanyID = dto.CompanyID;
            product.CategoryID = dto.CategoryID;
            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.UnitPrice = (decimal?)dto.UnitPrice;
            product.StockQuantity = dto.StockQuantity;
            product.Image = dto.Image;
            product.Status = dto.Status;
            product.CreatedDate = dto.CreatedDate;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Cập nhật sản phẩm thành công!",
                product.ProductID
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm với ID đã cho.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Xóa sản phẩm thành công!",
                product.ProductID
            });
        }

    }
}
