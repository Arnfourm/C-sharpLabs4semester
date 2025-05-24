using Microsoft.AspNetCore.Mvc;
using ProductModel.Models;
using task_beta.Models;

namespace task_beta.Controllers
{
    public class groupWorkController : Controller
    {
        private readonly ApplicationContext db;

        public groupWorkController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GroupWork()
        {
            return View();
        }

        public Product product;

        [HttpPost]
        public IActionResult ChangePrice(string type, int percent)
        {
            List<Product> productsToUpdate;

            if (type == "all")
            {
                productsToUpdate = db.products.ToList();
            }
            else
            {
                productsToUpdate = db.products.Where(p => p.ProductType == type).ToList();
            }

            if (productsToUpdate.Any())
            {
                foreach (var product in productsToUpdate)
                {
                    product.ProductCost = (int)Math.Round(product.ProductCost * (1 + percent / 100.0));
                }

                db.SaveChanges();
            }

            return RedirectToAction("GroupWork", "GroupWork");
        }
    }
}
