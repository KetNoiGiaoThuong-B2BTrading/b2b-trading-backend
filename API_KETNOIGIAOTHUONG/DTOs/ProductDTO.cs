using System.ComponentModel.DataAnnotations;

namespace API_KETNOIGIAOTHUONG.DTOs.Product
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập ID công ty")]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ID danh mục")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0")]
        public int StockQuantity { get; set; }

        public string Image { get; set; }
    }

    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0")]
        public int StockQuantity { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trạng thái sản phẩm")]
        public string Status { get; set; }
    }

    public class ProductResponseDTO
    {
        public int ProductID { get; set; }
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalNotes { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
    }
}
