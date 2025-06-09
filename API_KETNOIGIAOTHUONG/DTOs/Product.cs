namespace API_KETNOIGIAOTHUONG.DTOs.Product
{
    public class CreateProductDTO
    {
        //public int CompanyID { get; set; }
        //public int CategoryID { get; set; }
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
           public int CategoryID { get; set; }
   public int CompanyID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        //public int? ApprovedBy { get; set; }
        //public string ApprovalNotes { get; set; }

        // Thông tin công ty chi tiết
        //public CompanyDTO Company { get; set; }

        //// Thông tin danh mục chi tiết
        //public CategoryDTO Category { get; set; }

        //// Thông tin người duyệt chi tiết
        //public UserAccountDTO ApprovedByUser { get; set; }

    }



    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public CategoryDTO ParentCategory { get; set; }
        public string ImageCategory { get; set; }
    }

    public class CompanyDTO
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string BusinessSector { get; set; }
        public string Address { get; set; }
        public string Representative { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string VerificationStatus { get; set; }
        public string LegalDocuments { get; set; }
        public string ImageCompany { get; set; }
    }

    public class UserAccountDTO
    {
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }

}
