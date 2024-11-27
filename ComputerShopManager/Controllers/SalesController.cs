using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaleManagement.Services;

namespace ComputerShopManager.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly string _connectionString =  "Data Source=ComputerShopManager.db;Version=3;";

        [HttpPost("query")]
        public async Task<IActionResult> ExecuteQuery([FromBody] QueryRequest queryRequest)
        {
            List<SaleTransaction1> transactions = new List<SaleTransaction1>();
            string query = queryRequest.Query;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var transaction = new SaleTransaction1
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Quantity =Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDouble(reader["Price"])
                            };
                            transactions.Add(transaction);
                        }
                    }
                }
            }

            return Ok(transactions);
        }
    }

    public class QueryRequest
    {
        public string Query { get; set; }
    }
    public class SaleTransaction1
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
    }
}
