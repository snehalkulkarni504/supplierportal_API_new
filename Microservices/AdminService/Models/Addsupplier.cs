namespace AdminService.Models
{
    public class Addsupplier
    {
        public string? SupplierCode { get; set; }
        public string? SupplierName { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
}
