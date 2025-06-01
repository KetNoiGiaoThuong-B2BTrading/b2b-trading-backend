namespace API_KETNOIGIAOTHUONG.DTOs
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public string ImageCategoly { get; set; }
    }
}
