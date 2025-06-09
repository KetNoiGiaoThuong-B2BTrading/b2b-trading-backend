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

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductID)
                return BadRequest("ID không khớp");

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Status = product.Status;
            existingProduct.Image = product.Image;


            existingProduct.CreatedDate = product.CreatedDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
