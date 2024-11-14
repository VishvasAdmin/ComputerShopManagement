using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerShopManagerUI.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnPostGoToStock()
        {
            // Redirects to Stock management page
            return RedirectToPage("/Stock");
        }

        public IActionResult OnPostGoToSale()
        {
            // Redirects to Sale management page
            return RedirectToPage("/Sales");
        }

        public IActionResult OnPostGoToReport()
        {
            // Redirects to Report generation page
            return RedirectToPage("/Report");
        }
    }
}
