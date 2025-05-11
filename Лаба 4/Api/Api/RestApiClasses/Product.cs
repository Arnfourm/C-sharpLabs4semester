namespace Api.RestApiClasses
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public int ProductCost { get; set; }
        public required string ProductImgPath { get; set; }
    }
}