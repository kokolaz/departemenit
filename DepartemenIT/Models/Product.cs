using System.ComponentModel.DataAnnotations;

namespace DepartemenIT.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
