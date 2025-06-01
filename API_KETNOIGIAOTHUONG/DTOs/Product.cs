namespace API_KETNOIGIAOTHUONG.DTOs.Product
{
    public class CreateProductDTO
    {
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
    }

    public class UpdateProductDTO
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }

    public class ProductResponseDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
    }
}
