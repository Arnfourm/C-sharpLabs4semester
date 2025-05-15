using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;
using ProductModel.Models;

namespace task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connection;

        public HomeController(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("MySqlConnection");
        }

        private List<Product> products = new List<Product>();

        [BindProperty]
        public Order order { get; set; } = new Order();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            products.Clear();

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

            ViewBag.Products = products;
            ViewBag.succesValid = successValid;
            ViewBag.errorValid = errorValid;

            return View();
        }

        private string successValid = "";
        private string errorValid = "";

        [HttpPost]
        public IActionResult Order(Order order)
        {
            if (!ModelState.IsValid)
            {
                errorValid = "��������� ������ ���� ������� �����������!";
                return RedirectToAction("Index");
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

            successValid = $"������� {order.NameOrder} {order.SecondNameOrder}, ����� �������� �������!{Environment.NewLine}" +
               $"�������� ����� �� ������ {order.AddressOrder} � ���������� {order.QuantityOrder} ��.{Environment.NewLine}" +
               $"����������� ��� ������ � ��������� ����� �� ����� {order.EmailOrder}";

            return RedirectToAction("Index");
        }
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
