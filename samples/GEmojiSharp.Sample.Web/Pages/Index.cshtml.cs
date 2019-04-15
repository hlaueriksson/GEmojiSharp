using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Emoji { get; set; }
        public string Content { get; set; }

        public void OnGet()
        {
            Emoji = ":open_umbrella:";
            Content = "it's raining :cat:s and :dog:s!";
        }
    }
}