using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmallRetail.Data.Models
{
    public class User
    {
        public Guid? Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [MinLength(8)]
        [Required]
        public string? Password { get; set; }

        public string? Name { get; set; }

        [Column(TypeName = "varchar(10)")]
        public UserType? Type { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public enum UserType
    {
        Admin,
        User
    }
}