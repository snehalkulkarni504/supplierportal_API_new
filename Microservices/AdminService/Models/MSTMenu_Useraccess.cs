
namespace AdminService.Models
{
    public class MSTMenu_Useraccess
    {

        public string? routeLink { get; set; }
        public string? icon { get; set; }
        public string label { get; set; }
        public Boolean expanded { get; set; }
        public List<MSTMenu_Useraccess> items { get; set; }

        public int value { get; set; }
        public string? Menu { get; set; }
      
        public bool? Status { get; set; }
        public string? MenuCategory { get; set; }
       
       
      
      
    }
}
