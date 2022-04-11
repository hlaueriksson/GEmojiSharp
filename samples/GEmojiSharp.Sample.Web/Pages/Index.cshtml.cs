using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string? Emoji { get; set; }
        public string? Text { get; set; }
        [BindProperty]
        public string? Input { get; set; }
        public string? Output { get; set; }

        public void OnGet(string? input)
        {
            Emoji = ":open_umbrella:";
            Text = "it's raining :cat:s and :dog:s!";
            Input = input ?? "Hello, 🌍!";
            Output = input?.Demojify();
        }

        public IActionResult OnPostAsync()
        {
            return RedirectToPage("Index", new { input = Input });
        }
    }
}
