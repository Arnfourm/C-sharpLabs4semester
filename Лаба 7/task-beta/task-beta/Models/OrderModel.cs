using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderModel.Models
{
    public class Order
    {
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string? OrderName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string? OrderSurname { get; set; }

        [Required(ErrorMessage = "Почтовый ящик обязателен!")]
        [EmailAddress(ErrorMessage = "Некорректый адрес")]
        public string? OrderEmail { get; set; }

        [Required(ErrorMessage = "Адрес обязателен!")]
        public string? OrderAddress { get; set; }

        [Required(ErrorMessage = "Количество обязательно!")]
        [Range(1, 10, ErrorMessage = "Недопустимое количество")]
        public int? OrderQuantity { get; set; }
    }
}
