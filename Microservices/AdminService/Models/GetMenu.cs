namespace SupplierService.Models
{
    public class GetMenu
    {
        public int MenuId { get; set; }
        public string? Menu { get; set; }
        public string? SubMenu { get; set; }
        public bool? Status { get; set; }
        public string? MenuCategory { get; set; }
        public string? Navigate_Url { get; set; }
        public string Icon { get; set; }
        public List<GetMenu> submenulist { get; set; }
    }
}
