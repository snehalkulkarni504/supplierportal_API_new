namespace SupplierService.Models
{
    public class AddRoleData
    {
        public string Role_name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? CreatedBy { get; set; }

        public int[] MenuId { get; set; }

        public string? Created_Date { get; set; }
    }
}
