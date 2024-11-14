using System.Data.SQLite;
namespace ReportGeneration.Services
{
    public class WeeklyReportService
    {
         private readonly string _connectionString = "Data Source=ComputerShopManager.db;Version=3;";

        public List<StockReport> GenerateStockReport()
        {
            var stockReport = new List<StockReport>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Quantity, Price FROM StockItem;";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stockReport.Add(new StockReport
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Price = Convert.ToDouble(reader["Price"])
                        });
                    }
                }
            }
            return stockReport;
        }

        public List<SaleReport> GenerateSalesReport()
        {
            var salesReport = new List<SaleReport>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT SaleTransaction.Id, StockItem.Name, SaleTransaction.Quantity, SaleTransaction.SaleDate
                                 FROM SaleTransaction
                                 JOIN StockItem ON SaleTransaction.StockItemId = StockItem.Id;";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesReport.Add(new SaleReport
                        {
                            SaleId = Convert.ToInt32(reader["Id"]),
                            ItemName = reader["Name"].ToString(),
                            QuantitySold = Convert.ToInt32(reader["Quantity"]),
                            SaleDate = Convert.ToDateTime(reader["SaleDate"])
                        });
                    }
                }
            }
            return salesReport;
        }
    
    }
}
