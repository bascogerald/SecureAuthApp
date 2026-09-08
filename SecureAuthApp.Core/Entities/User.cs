using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAuthApp.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        //we never save real passwords. we save a scrabled hash
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // User, Admin
    }
}
