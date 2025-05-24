using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderModel.Models
{
    public class Order
    {
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "��� �����������!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "�� ����� 2 � �� ����� 50 ��������")]
        public string? OrderName { get; set; }

        [Required(ErrorMessage = "������� �����������!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "�� ����� 2 � �� ����� 50 ��������")]
        public string? OrderSurname { get; set; }

        [Required(ErrorMessage = "�������� ���� ����������!")]
        [EmailAddress(ErrorMessage = "����������� �����")]
        public string? OrderEmail { get; set; }

        [Required(ErrorMessage = "����� ����������!")]
        public string? OrderAddress { get; set; }

        [Required(ErrorMessage = "���������� �����������!")]
        [Range(1, 10, ErrorMessage = "������������ ����������")]
        public int? OrderQuantity { get; set; }
    }
}
