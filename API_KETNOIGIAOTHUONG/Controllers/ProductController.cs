// Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using API_KETNOIGIAOTHUONG.DTOs.Product;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // API tạo sản phẩm mới
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] CreateProductDTO dto)
        {
            return Ok("Tạo sản phẩm thành công");
        }

        // API lấy thông tin sản phẩm theo ID
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new ProductResponseDTO { ProductID = id });
        }

        // API cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDTO dto)
        {
            return Ok("Cập nhật sản phẩm thành công");
        }

        // API xoá sản phẩm
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok("Xoá sản phẩm thành công");
        }

        // API danh sách sản phẩm theo công ty
        [HttpGet("company/{companyId}")]
        public IActionResult GetProductsByCompany(int companyId)
        {
            return Ok();
        }
    }
}