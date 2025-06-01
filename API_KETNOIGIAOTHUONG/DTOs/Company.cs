namespace API_KETNOIGIAOTHUONG.DTOs.Company
{
    public class CreateCompanyDTO
    {
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string BusinessSector { get; set; }
        public string Address { get; set; }
        public string Representative { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationStatus { get; set; }
        public string LegalDocuments { get; set; }

        public string ImageCompany {  get; set; }
    }

    public class CompanyResponseDTO
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string BusinessSector { get; set; }
        public string Address { get; set; }
        public string Representative { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationStatus { get; set; }

        public string ImageCompany { get; set; }
    }
}
