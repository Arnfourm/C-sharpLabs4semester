using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModel.Models
{
    public class Product
    {
        [Column("ProductId")]
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public int ProductCost { get; set; }
        public required string ProductIMG { get; set; }
        public required string ProductType { get; set; }
    }
}