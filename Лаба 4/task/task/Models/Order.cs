using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task2.Models
{
    public class Order
    {
        [Required(ErrorMessage = "Имя обязательно!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string NameOrder { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string SecondNameOrder { get; set; }

        [Required(ErrorMessage = "Почтовый ящик обязателен!")]
        [EmailAddress(ErrorMessage = "Некорректый адрес")]
        public string EmailOrder { get; set; }

        [Required(ErrorMessage = "Адрес обязателен!")]
        public string AddressOrder { get; set; }

        [Required(ErrorMessage = "Количество обязательно!")]
        [Range(1, 10, ErrorMessage = "Недопустимое количество")]
        public int QuantityOrder { get; set; }
    }

    public class OrderList
    {
        [JsonPropertyName("orderId")]
        public int orderListId { get; set; }

        [JsonPropertyName("orderName")]
        public required string nameCustomer { get; set; }

        [JsonPropertyName("orderSurname")]
        public required string secondNameCustomer { get; set; }

        [JsonPropertyName("orderEmail")]
        public required string mailCustomer { get; set; }

        [JsonPropertyName("orderAddress")]
        public required string addressCustomer { get; set; }

        [JsonPropertyName("orderQuantity")]
        public int quantityCustomer { get; set; }
    }
}
