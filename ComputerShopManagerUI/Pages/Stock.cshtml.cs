using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class StockModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public StockModel(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    [BindProperty]
    public string StockItemName { get; set; }
    
    [BindProperty]
    public int Quantity { get; set; }

    [BindProperty]
    public double Price{get;set;}
    
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _clientFactory.CreateClient("ComputerShopAPI");
        var stockItem = new { StockItemName, Quantity, Price};
        var content = new StringContent(JsonSerializer.Serialize(stockItem), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Stock/add", content);
        if (response.IsSuccessStatusCode)
            Message = "Stock item added successfully!";
        else
            Message = "Failed to add stock item.";

        return Page();
    }
}
