using System; 
using System.ComponentModel.DataAnnotations; 

namespace WebApplication2.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Lejohen vetem karakter alfabetik.")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Range(0, 999,
  ErrorMessage = "Kjo fushe lejon vetem numra ne mes 0 dhe 999.")]
        public double Weight { get; set; }
    }
}
