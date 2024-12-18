namespace AdminService.Models
{
    public class Adduser
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int? SupplierId { get; set; }
    }
}
