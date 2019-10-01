using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Emoji { get; set; }
        public string Text { get; set; }

        public void OnGet()
        {
            Emoji = ":open_umbrella:";
            Text = "it's raining :cat:s and :dog:s!";
        }
    }
}