using System.Text.Json.Serialization;

public class Product
{
    [JsonPropertyName("productId")]
    public int IdProduct { get; set; }

    [JsonPropertyName("productName")]
    public required string NameProduct { get; set; }

    [JsonPropertyName("productCost")]
    public int CostProduct { get; set; }

    [JsonPropertyName("productImgPath")]
    public required string imagePathProduct { get; set; }
}
