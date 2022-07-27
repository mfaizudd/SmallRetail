using System;

namespace SmallRetail.Web.Resources
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}