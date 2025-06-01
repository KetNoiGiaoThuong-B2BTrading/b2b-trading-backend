namespace API_KETNOIGIAOTHUONG.DTOs.Quotation
{
    public class CreateQuotationRequestDTO
    {
        public int ProductID { get; set; }
        public int BuyerID { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }

    public class QuotationResponseDTO
    {
        public int QuotationID { get; set; }
        public string ProductName { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
    }
}
