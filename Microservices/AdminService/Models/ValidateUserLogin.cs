﻿namespace AdminService.Models
{
    public class ValidateUserLogin
    {
        public int mst_user_id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
    }

}