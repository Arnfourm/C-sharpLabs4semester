using System.ComponentModel.DataAnnotations;

namespace OrderModel.Models
{
    public class Order
    {
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "��� �����������!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "�� ����� 2 � �� ����� 50 ��������")]
        public string NameOrder { get; set; }

        [Required(ErrorMessage = "������� �����������!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "�� ����� 2 � �� ����� 50 ��������")]
        public string SecondNameOrder { get; set; }

        [Required(ErrorMessage = "�������� ���� ����������!")]
        [EmailAddress(ErrorMessage = "����������� �����")]
        public string EmailOrder { get; set; }

        [Required(ErrorMessage = "����� ����������!")]
        public string AddressOrder { get; set; }

        [Required(ErrorMessage = "���������� �����������!")]
        [Range(1, 10, ErrorMessage = "������������ ����������")]
        public int QuantityOrder { get; set; }
    }
}
