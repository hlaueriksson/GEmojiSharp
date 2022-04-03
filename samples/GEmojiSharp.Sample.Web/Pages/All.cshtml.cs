using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class AllModel : PageModel
    {
        public IEnumerable<IGrouping<string, GEmoji>>? Categories { get; set; }

        public void OnGet()
        {
            Categories = Emoji.All.GroupBy(x => x.Category);
        }
    }
}
