namespace AdminService.Models
{
    public class GetSupplierDetails
    {
        public int SupplierId { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierName { get; set; }
        public string? Country { get; set; }
        public int CountryId { get; set; }
        public string? CreatedBy { get; set; }
        public int mst_user_id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Status { get; set; }
    }
}
