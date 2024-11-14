using StockManager.Services;

namespace SaleManagement.Services
{
    public class StockServiceIntegration
    {
        private readonly StockService _stockService;

        public StockServiceIntegration(StockService stockService)
        {
            _stockService = stockService;
        }

        public void CheckStockAvailability(int stockItemId)
        {
            // Check stock logic
        }
    }
}
