using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using Api.RestApiClasses;

namespace Api.Constrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            IList<Product> productSpecs = new List<Product>();

            string sqlCommandProduct = "select * from product";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProduct, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        productSpecs.Add(new Product
                        {
                            ProductId = sqlSelectProduct.GetInt32("ProductId"),
                            ProductName = sqlSelectProduct.GetString("ProductName"),
                            ProductCost = sqlSelectProduct.GetInt32("ProductCost"),
                            ProductImgPath = sqlSelectProduct.GetString("ProductIMG")
                        });
                    }
                }
            }

            return productSpecs;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product productUnit = null;
            string sqlCommandProductSingle = "select * from product where ProductId = @idProductSingle";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();

                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProductSingle, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@idProductSingle", id);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    if (sqlSelectProduct.Read())
                    {
                        productUnit = new Product
                        {
                            ProductId = sqlSelectProduct.GetInt32("ProductId"),
                            ProductName = sqlSelectProduct.GetString("ProductName"),
                            ProductCost = sqlSelectProduct.GetInt32("ProductCost"),
                            ProductImgPath = sqlSelectProduct.GetString("ProductIMG")
                        };
                    }
                };
            };

             return productUnit;
        }
    }
}
