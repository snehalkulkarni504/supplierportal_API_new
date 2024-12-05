using System.Data;

namespace AdminService.Models
{
    public class Usermaster
    {

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Role { get; set; }
        public int MST_Role_Id { get; set; }
        public string Status { get; set; }
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime created_date { get; set; }
    }
}
