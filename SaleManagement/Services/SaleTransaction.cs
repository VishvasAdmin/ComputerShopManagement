using System;

namespace SaleManagement.Services
{
    public class SaleTransaction
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
