using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmallRetail.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [MinLength(8)]
        [Required]
        public string Password { get; set; }
        
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}