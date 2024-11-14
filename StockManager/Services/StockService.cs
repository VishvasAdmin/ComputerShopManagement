using System;
using System.Data.SQLite;
using StockManager.Models;

namespace StockManager.Services
{
    public class StockService
    {
        private readonly string _connectionString = "Data Source=ComputerShopManager.db;Version=3;";

        public StockService()
        {
            InitializeDatabase();
        }

        // Create the table if it doesn't exist
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string tableQuery = @"CREATE TABLE IF NOT EXISTS StockItem (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Quantity INTEGER,
                                        Price REAL
                                    );";
                using (var command = new SQLiteCommand(tableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add a new stock item
        public void AddStockItem(StockItem stockItem)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO StockItem (Name, Quantity, Price) VALUES (@Name, @Quantity, @Price);";
                
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", stockItem.Name);
                    command.Parameters.AddWithValue("@Quantity", stockItem.Quantity);
                    command.Parameters.AddWithValue("@Price", stockItem.Price);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
