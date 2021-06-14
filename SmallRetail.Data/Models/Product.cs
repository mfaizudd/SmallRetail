using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallRetail.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        public List<TransactionProduct> TransactionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}