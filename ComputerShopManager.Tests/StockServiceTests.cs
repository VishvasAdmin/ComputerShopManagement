using Rhino.Mocks;
using Xunit;
using StockManager.Services; // Replace with your actual namespace
using StockManager.Models; // Replace with your actual namespace

public class StockServiceTests
{
    private readonly StockService _stockService;
    private readonly IStockRepository _mockStockRepository;

    public StockServiceTests()
    {
        // Mock the IStockRepository using Rhino Mocks
        _mockStockRepository = MockRepository.GenerateMock<IStockRepository>();

        // Instantiate the service with the mock
        _stockService = new StockService(_mockStockRepository);
    }

    [Fact]
    public void ReduceStock_ShouldReturnTrue_WhenStockIsSufficient()
    {
        // Arrange
        int productId = 1;
        int quantityToReduce = 5;

        var stockItem = new StockItem
        {
            ProductId = productId,
            Quantity = 10
        };

        // Set up the mock to return a stock item with sufficient quantity
        _mockStockRepository.Stub(x => x.GetStockItem(productId)).Return(stockItem);

        // Act
        bool result = _stockService.ReduceStock(productId, quantityToReduce);

        // Assert
        Assert.True(result);
        Assert.Equal(5, stockItem.Quantity); // Stock should be reduced by 5
        _mockStockRepository.AssertWasCalled(x => x.Save(stockItem)); // Ensure Save was called
    }

    [Fact]
    public void ReduceStock_ShouldReturnFalse_WhenStockIsInsufficient()
    {
        // Arrange
        int productId = 2;
        int quantityToReduce = 15;

        var stockItem = new StockItem
        {
            ProductId = productId,
            Quantity = 10
        };

        // Set up the mock to return a stock item with insufficient quantity
        _mockStockRepository.Stub(x => x.GetStockItem(productId)).Return(stockItem);

        // Act
        bool result = _stockService.ReduceStock(productId, quantityToReduce);

        // Assert
        Assert.False(result);
        _mockStockRepository.AssertWasNotCalled(x => x.Save(Arg<StockItem>.Is.Anything)); // Save should not be called
    }
}
