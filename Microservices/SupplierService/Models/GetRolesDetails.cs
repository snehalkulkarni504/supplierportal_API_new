namespace SupplierService.Models
{
    public class GetRolesDetails
    {
        public int menu_id { get; set; }
        public int id { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public string? Menu { get; set; }
        public string Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? Created_Date { get; set; }
    }
}
