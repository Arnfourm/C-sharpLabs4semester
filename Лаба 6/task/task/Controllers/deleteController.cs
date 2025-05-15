using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using task.Models;

namespace task_beta.Controllers
{
    //[Authorize]
    [Authorize(Roles = "Admin")]
    public class deleteController : Controller
    {
        private string connection;
        
        public deleteController(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("MySqlConnection");
        }

        public Order order = new Order();

        [HttpGet]
        public IActionResult Deleter()
        {
            Console.WriteLine("Пользователь авторизован: " + User.Identity.IsAuthenticated);
            Console.WriteLine("Имя пользователя: " + User.Identity.Name);

            
            //var userRoles = await _userManager.GetRolesAsync(user);
            //var allRoles = _roleManager.Roles.ToList();



            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"CLAIM => Type: {claim.Type}, Value: {claim.Value}");
            }

            Console.WriteLine("User.IsInRole(\"Admin\"): " + User.IsInRole("Admin"));

            return View();
        }

        [HttpPost]
        public IActionResult DeleterPost(Order order)
        {
            string sqlCommand = "DELETE FROM orderinfo WHERE OrderId = @idOrder";

            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommand, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@idOrder", order.IdOrder);

                commandCurrent.ExecuteNonQuery();
                connectionCurrent.Close();
            }

            return RedirectToAction("Deleter", "Delete");
        }
    }
}
