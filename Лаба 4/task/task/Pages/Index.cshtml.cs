using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using task2.Models;

namespace calculator.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private HttpClient httpClient = new HttpClient();

        private string apiUrlProducts = "https://localhost:7152/api/Products";
        private string apiUrlOrders = "https://localhost:7152/api/Orders";

        public List<Product> products = new List<Product>();
        public static List<OrderList> orders = new List<OrderList>();
        [BindProperty]
        public Order order { get; set; } = new Order();

        public async void OnGet()
        {
            products.Clear();
            orders.Clear();

            var responseGetProducts = httpClient.GetAsync(apiUrlProducts).Result;
            if (responseGetProducts.IsSuccessStatusCode)
            {
                products = await responseGetProducts.Content.ReadFromJsonAsync<List<Product>>();
            }

            var responseGetOrders = httpClient.GetAsync(apiUrlOrders).Result;
            if (responseGetOrders.IsSuccessStatusCode)
            {
                orders = await responseGetOrders.Content.ReadFromJsonAsync<List<OrderList>>();
            }
        }


        public string successValid = "";
        public string errorValid = "";
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

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
