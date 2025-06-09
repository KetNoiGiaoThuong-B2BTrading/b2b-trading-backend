using System.ComponentModel.DataAnnotations;

namespace API_KETNOIGIAOTHUONG.Models.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }

        public string ImageCategoly { get; set; }
    }

    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }

        public string ImageCategoly { get; set; }
    }
} 