using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LiteEcommerceApi.Models
{
    [Index(nameof(ProductCode), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        public string ProductCode { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string ProductName { get; set; }

        public string? Image { get; set; }

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public int Quantity { get; set; }

        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
