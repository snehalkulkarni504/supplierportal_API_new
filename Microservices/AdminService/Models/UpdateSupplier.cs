namespace AdminService.Models
{
    public class UpdateSupplier
    {
        public int SupplierId { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierName { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        //public int ModifiedBy { get; set; }
    }
}
