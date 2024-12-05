namespace AdminService.Models
{
    public class UpdateRoleData
    {
        public string? MenuId { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string? Username { get; set; }
        public int IsActive { get; set; }
        public string? Menu { get; set; }
    }
}
