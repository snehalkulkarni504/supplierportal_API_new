﻿namespace SupplierService.Models
{
    public class Updateuser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
