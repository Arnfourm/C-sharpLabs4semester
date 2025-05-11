using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using task2.Models;

namespace calculator.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        public List<Product> products = new List<Product>();
        public static List<OrderList> orders = new List<OrderList>();
        [BindProperty]
        public Order order { get; set; } = new Order();

        public void OnGet()
        {
            products.Clear();
            orders.Clear();

            string sqlCommandProduct = "select * from product";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProduct, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        products.Add(new Product
                        {
                            IdProduct = sqlSelectProduct.GetInt32("ProductId"),
                            NameProduct = sqlSelectProduct.GetString("ProductName"),
                            CostProduct = sqlSelectProduct.GetInt32("ProductCost"),
                            imagePathProduct = sqlSelectProduct.GetString("ProductIMG")
                        });
                    }
                }
            }

            string sqlCommandOrder = "select * from orderinfo";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandOrder, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        orders.Add(new OrderList
                        {
                            orderListId = sqlSelectProduct.GetInt32("OrderId"),
                            nameCustomer = sqlSelectProduct.GetString("OrderName"),
                            secondNameCustomer = sqlSelectProduct.GetString("OrderSurname"),
                            mailCustomer = sqlSelectProduct.GetString("OrderEmail"),
                            addressCustomer = sqlSelectProduct.GetString("OrderAddress"),
                            quantityCustomer = sqlSelectProduct.GetInt32("OrderQuantity"),
                        });
                    }
                }
            }
        }


        public string successValid = "";
        public string errorValid = "";
        

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorValid = "Некоторые данные были введены неправильно!";
                return;
            }

            string sqlCommand = "insert into orderinfo (OrderName, OrderSurname, OrderEmail, OrderAddress, OrderQuantity)" +
                                "values (@NameOrder, @SecondNameOrder,  @EmailOrder, @AddressOrder, @QuantityOrder); ";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommand, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@NameOrder", order.NameOrder);
                commandCurrent.Parameters.AddWithValue("@SecondNameOrder", order.SecondNameOrder);
                commandCurrent.Parameters.AddWithValue("@EmailOrder", order.EmailOrder);
                commandCurrent.Parameters.AddWithValue("@AddressOrder", order.AddressOrder);
                commandCurrent.Parameters.AddWithValue("@QuantityOrder", order.QuantityOrder);

                commandCurrent.ExecuteNonQuery();
                connectionCurrent.Close();
            }

            OnGet();

            successValid = $"Спасибо {order.NameOrder} {order.SecondNameOrder}, заказ выполнен успешно!{Environment.NewLine}" +
               $"Доставим товар по адресу {order.AddressOrder} в количестве {order.QuantityOrder} шт.{Environment.NewLine}" +
               $"Электронный чек придет в ближайшее время на почту {order.EmailOrder}";
            ;
        }
    }
}
