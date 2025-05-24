using Microsoft.AspNetCore.Mvc;
using OrderModel.Models;
using task_beta.Models;

namespace task_beta.Controllers
{
    public class deleteController : Controller
    {
        private readonly ApplicationContext db;

        public deleteController(ApplicationContext db)
        {
            this.db = db;
        }

        public Order order = new Order();

        [HttpGet]
        public IActionResult Deleter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleterPost(Order order)
        {
            var currentOrder = db.orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();
            db.orders.Remove(currentOrder);
            db.SaveChanges();

            return RedirectToAction("Deleter", "Delete");
        }
    }
}
