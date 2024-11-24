using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

public class ReportModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public ReportModel(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    [BindProperty]
    public string ReportType { get; set; }

    public string ReportData { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
        var client = _clientFactory.CreateClient("ComputerShopAPI");
            var url = string.Empty;
if (ReportType == "Sale")   
    url = "/api/Report/sales";
else
    url = "/api/Report/stock";

var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
            ReportData = await response.Content.ReadAsStringAsync();
        else
            ReportData = "Failed to fetch report.";

        return Page();
    }
}
