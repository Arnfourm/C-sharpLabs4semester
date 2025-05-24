using Microsoft.AspNetCore.Mvc;
using task_beta.Models;
using ProductModel.Models;

namespace task_beta.Controllers
{
    public class watchController : Controller
    {
        private readonly ApplicationContext db;

        public watchController(IConfiguration configuration, ApplicationContext db)
        {
            this.db = db;
        }

        private static List<Product> products = new List<Product>();

        public IActionResult Observer()
        {
            products.Clear();

            products = db.products.ToList();

            ViewBag.Products = products;

            return View();
        }
    }
}
