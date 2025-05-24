    using Microsoft.AspNetCore.Mvc;
    using OrderModel.Models;
    using task_beta.Models;

    namespace task_beta.Controllers
    {
        public class editController : Controller
        {
            private readonly ApplicationContext db;

            public editController(ApplicationContext db)
            {
                this.db = db;
            }

            public Order order { get; set; } = new Order();

            [HttpGet]
            public IActionResult Editor()
            {
                return View();
            }

            [HttpPost]
            public IActionResult EditorPost(Order order)
            {
                var currentOrder = db.orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();
                if (currentOrder != null)
                {
                    currentOrder.OrderName = order.OrderName;
                    currentOrder.OrderSurname = order.OrderSurname;
                    currentOrder.OrderEmail = order.OrderEmail;
                    currentOrder.OrderAddress = order.OrderAddress;
                    currentOrder.OrderQuantity = order.OrderQuantity;
                    db.orders.Update(currentOrder);
                    db.SaveChanges();
                }

                return RedirectToAction("Editor", "Edit");
            }
        }
    }