namespace Api.RestApiClasses
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public required string OrderName { get; set; }
        public required string OrderSurname { get; set; }
        public required string OrderEmail { get; set; }
        public int OrderQuantity { get; set; }
        public required string OrderAddress {  get; set; }
    }
}
