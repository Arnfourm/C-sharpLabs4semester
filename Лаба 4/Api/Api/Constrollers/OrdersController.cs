using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using Api.RestApiClasses;

namespace Api.Constrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        // GET: api/<OrderInfoController>
        [HttpGet]
        public IEnumerable<OrderInfo> Get()
        {
            IList<OrderInfo> OrderInfoSpecs = new List<OrderInfo>();

            string sqlCommandProduct = "select * from orderInfo";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProduct, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        OrderInfoSpecs.Add(new OrderInfo
                        {
                            OrderId = sqlSelectProduct.GetInt32("OrderId"),
                            OrderName = sqlSelectProduct.GetString("OrderName"),
                            OrderSurname = sqlSelectProduct.GetString("OrderSurname"),
                            OrderEmail = sqlSelectProduct.GetString("OrderEmail"),
                            OrderQuantity = sqlSelectProduct.GetInt32("OrderQuantity"),
                            OrderAddress = sqlSelectProduct.GetString("OrderAddress")
                        });
                    }
                }
            }

            return OrderInfoSpecs;
        }

        // GET api/<OrderInfoController>/5
        [HttpGet("{id}")]
        public OrderInfo Get(int id)
        {
            OrderInfo orderInfoUnit = null;
            string sqlCommandProductSingle = "select * from orderInfo where OrderId = @idOrder";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();

                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProductSingle, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@idOrder", id);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    if (sqlSelectProduct.Read())
                    {
                        orderInfoUnit = new OrderInfo
                        {
                            OrderId = sqlSelectProduct.GetInt32("OrderId"),
                            OrderName = sqlSelectProduct.GetString("OrderName"),
                            OrderSurname = sqlSelectProduct.GetString("OrderSurname"),
                            OrderEmail = sqlSelectProduct.GetString("OrderEmail"),
                            OrderQuantity = sqlSelectProduct.GetInt32("OrderQuantity"),
                            OrderAddress = sqlSelectProduct.GetString("OrderAddress")
                        };
                    }
                };
            };

            return orderInfoUnit;
        }
    }
}
