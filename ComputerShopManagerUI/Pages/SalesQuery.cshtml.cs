using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SalesqModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public SalesqModel(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

   
    
    [BindProperty]
    public String Query { get; set; }
    
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _clientFactory.CreateClient("ComputerShopAPI");
        var QueryRequest = new {Query};
        var content = new StringContent(JsonSerializer.Serialize(QueryRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/sales/query", content);
       // var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
            Message = await response.Content.ReadAsStringAsync();
        else
            Message = "Failed, unable to fetch the data";

        return Page();
    }


}
