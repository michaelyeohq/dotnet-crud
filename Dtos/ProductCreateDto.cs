using System.ComponentModel.DataAnnotations;

namespace Store.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}