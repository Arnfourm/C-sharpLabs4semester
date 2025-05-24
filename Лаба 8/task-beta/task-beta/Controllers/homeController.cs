using Microsoft.AspNetCore.Mvc;
using OrderModel.Models;
using ProductModel.Models;
using task_beta.Models;

namespace task_beta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;

        public HomeController(ApplicationContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Order order { get; set; } = new Order();

        [HttpGet]
        public async Task<IActionResult> Index(string selectType = "All")
        {
            List<Product> products;

            products = db.products.ToList();

            ViewBag.Products = products;
            ViewBag.SelectedType = selectType;
            ViewBag.succesValid = successValid;
            ViewBag.errorValid = errorValid;

            return View();
        }

        [HttpGet]
        public IActionResult GetProductsByType(string selectType = "All")
        {
            List<Product> products;

            if (selectType == "All")
                products = db.products.ToList();
            else
                products = db.products.Where(p => p.ProductType == selectType).ToList();

            return PartialView("ProductListPartial", products);
        }

        private string successValid = "";
        private string errorValid = "";

        [HttpPost]
        public async Task<IActionResult> Order(Order order)
        {
            if (!ModelState.IsValid)
            {
                errorValid = "Некоторые данные были введены неправильно!";
                return RedirectToAction("Index");
            }

            db.orders.Add(new Order()
            {
                OrderName = order.OrderName,
                OrderSurname = order.OrderSurname,
                OrderAddress = order.OrderAddress,
                OrderQuantity = order.OrderQuantity
            });

            await db.SaveChangesAsync();

            successValid = $"Спасибо {order.OrderName} {order.OrderSurname}, заказ выполнен успешно!{Environment.NewLine}" +
               $"Доставим товар по адресу {order.OrderAddress} в количестве {order.OrderQuantity} шт.{Environment.NewLine}" +
               $"Электронный чек придет в ближайшее время на почту {order.OrderEmail}";

            return RedirectToAction("Index");
        }
    }
}
