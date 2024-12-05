namespace SupplierService.Models
{
    public class UpdateRoleData
    {
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public int[] MenuId { get; set; }
    }
}
