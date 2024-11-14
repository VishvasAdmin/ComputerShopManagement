using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SalesModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public SalesModel(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    [BindProperty]
    public int StockItemId { get; set; }
    
    [BindProperty]
    public int Quantity { get; set; }
    
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _clientFactory.CreateClient("ComputerShopAPI");
        var saleItem = new { StockItemId, Quantity };
        var content = new StringContent(JsonSerializer.Serialize(saleItem), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Sale/add", content);
        if (response.IsSuccessStatusCode)
            Message = "Sale added successfully!";
        else
            Message = "Failed to add sale.";

        return Page();
    }
}
