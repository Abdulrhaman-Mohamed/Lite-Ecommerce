using System.ComponentModel.DataAnnotations;

namespace LiteEcommerceApi.Dots
{
    public class ProductDots
    {
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }

        public IFormFile? Image { get; set; }
        [Required]
        [Range(5,double.MaxValue)]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }
        [Required]
        public int Quantity { get; set; }

        public Guid Category { get; set; } 


    }
}
