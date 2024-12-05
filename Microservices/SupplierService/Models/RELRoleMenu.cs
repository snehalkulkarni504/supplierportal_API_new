namespace SupplierService.Models
{
    public partial class RELRoleMenu
    {
        public int Rel_RoleMenuId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
