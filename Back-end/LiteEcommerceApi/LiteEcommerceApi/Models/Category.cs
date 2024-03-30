using System.ComponentModel.DataAnnotations;

namespace LiteEcommerceApi.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}
