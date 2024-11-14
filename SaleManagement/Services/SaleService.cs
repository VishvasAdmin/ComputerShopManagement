using System;
using System.Data.SQLite;

namespace SaleManagement.Services
{
    public class SaleService
    {
        private readonly string _connectionString = "Data Source=ComputerShopManager.db;Version=3;";

        public SaleService()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string saleTableQuery = @"CREATE TABLE IF NOT EXISTS SaleTransaction (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            StockItemId INTEGER,
                                            Quantity INTEGER,
                                            SaleDate TEXT,
                                            FOREIGN KEY (StockItemId) REFERENCES StockItem (Id)
                                        );";
                using (var command = new SQLiteCommand(saleTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add a new sale transaction and deduct stock
        public string AddSaleTransaction(SaleTransaction saleTransaction)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Check if enough stock is available
                int currentStock = GetStockQuantity(saleTransaction.StockItemId);
                if (currentStock < saleTransaction.Quantity)
                {
                    return $"Insufficient stock. Available: {currentStock}, Requested: {saleTransaction.Quantity}.";
                }

                // Deduct the stock
                DeductStock(saleTransaction.StockItemId, saleTransaction.Quantity);

                // Record the sale transaction
                string insertQuery = "INSERT INTO SaleTransaction (StockItemId, Quantity, SaleDate) VALUES (@StockItemId, @Quantity, @SaleDate);";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@StockItemId", saleTransaction.StockItemId);
                    command.Parameters.AddWithValue("@Quantity", saleTransaction.Quantity);
                    command.Parameters.AddWithValue("@SaleDate", saleTransaction.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.ExecuteNonQuery();
                }
            }
            return "Sale transaction added successfully and stock updated.";
        }

        private int GetStockQuantity(int stockItemId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Quantity FROM StockItem WHERE Id = @StockItemId;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockItemId", stockItemId);
                    object result = command.ExecuteScalar();
                    return result == null ? 0 : Convert.ToInt32(result);
                }
            }
        }

        private void DeductStock(int stockItemId, int quantityToDeduct)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE StockItem SET Quantity = Quantity - @QuantityToDeduct WHERE Id = @StockItemId;";
                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@QuantityToDeduct", quantityToDeduct);
                    command.Parameters.AddWithValue("@StockItemId", stockItemId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
