using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace API_KETNOIGIAOTHUONG.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }

        [ForeignKey(nameof(ParentCategoryID))]
        public Category ParentCategory { get; set; }

        public string ImageCategoly { get; set; }

        // Navigation Properties
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
