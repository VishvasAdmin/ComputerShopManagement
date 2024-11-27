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
            List<SaleTransaction> transactions = new List<SaleTransaction>();
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
                            var transaction = new SaleTransaction
                            {
                                Id = reader.GetInt32(0),
                                StockItemId = reader.GetInt32(1),
                                Quantity = reader.GetInt32(2),
                                SaleDate = reader.GetDateTime(3),
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
}
