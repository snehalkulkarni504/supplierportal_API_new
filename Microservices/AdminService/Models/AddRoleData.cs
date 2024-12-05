namespace AdminService.Models
{
    public class AddRoleData
    {
        public string Role_name { get; set; }
        public string Description { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }

        public string MenuId { get; set; }

        public string? Created_Date { get; set; }
    }
}
