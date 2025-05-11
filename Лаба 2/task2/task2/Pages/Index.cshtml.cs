using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using task2.Models;

namespace calculator.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        public List<Product> products = new List<Product>();
        [BindProperty]
        public Order order { get; set; } = new Order();

        public void OnGet()
        {
            products.Add(new Product { IdProduct = 1, NameProduct = "Apple watch ulta 2", CostProduct = 700, imagePathProduct = "imgs/appleulta.png" });
            products.Add(new Product { IdProduct = 2, NameProduct = "Rolex Day-Date 36", CostProduct = 117250, imagePathProduct = "imgs/rolexDayDate36.png" });
            products.Add(new Product { IdProduct = 3, NameProduct = "King Seiko Automatic", CostProduct = 2000, imagePathProduct = "imgs/KingSeiko.png" });
            products.Add(new Product { IdProduct = 4, NameProduct = "Paket Philippe 5270/1R", CostProduct = 230000, imagePathProduct = "imgs/PatekPhilippe.png" });
            products.Add(new Product { IdProduct = 5, NameProduct = "Rolex DateJust 31", CostProduct = 51900, imagePathProduct = "imgs/rolexDateJust31.png" });
            products.Add(new Product { IdProduct = 6, NameProduct = "Patek Philippe 6300/400G", CostProduct = 9000000, imagePathProduct = "imgs/PatekPhilippeBlackWhite.png" });
            products.Add(new Product { IdProduct = 7, NameProduct = "Apple watch series 10", CostProduct = 450, imagePathProduct = "imgs/appleseries10.png" });
            products.Add(new Product { IdProduct = 8, NameProduct = "Jacob&Co Palatial Classic", CostProduct = 16500, imagePathProduct = "imgs/Jacob&Co.png" });
        }

        public string successValid = "";
        public string errorValid = "";
        public static List<OrderList> orders = new List<OrderList>();

        public void OnPost()
        {
            OnGet();

            if (!ModelState.IsValid)
            {
                errorValid = "Некоторые данные были введены неправильно!";
                return;
            }

            successValid = $"Спасибо {order.NameOrder} {order.SecondNameOrder}, заказ выполнен успешно!{Environment.NewLine}" +
               $"Доставим товар по адресу {order.AddressOrder} в количестве {order.QuantityOrder} шт.{Environment.NewLine}" +
               $"Электронный чек придет в ближайшее время на почту {order.EmailOrder}";
            ;
            orders.Add(new OrderList { orderListId = orders.Count + 1, nameCustomer = order.NameOrder, secondNameCustomer = order.SecondNameOrder, 
            mailCustomer = order.EmailOrder, addressCustomer = order.AddressOrder, quantityCustomer = order.QuantityOrder});
            ModelState.Clear();
        }
    }
}