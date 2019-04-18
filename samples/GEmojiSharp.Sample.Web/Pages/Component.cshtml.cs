using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class ComponentModel : PageModel
    {
        public string Emoji { get; set; }
        public string Content { get; set; }

        //private readonly ITagHelperComponentManager _tagHelperComponentManager;

        //public ComponentModel(ITagHelperComponentManager tagHelperComponentManager)
        //{
        //    _tagHelperComponentManager = tagHelperComponentManager;
        //}

        public void OnGet()
        {
            //_tagHelperComponentManager.Components.Add(new BodyTagHelperComponent());

            Emoji = ":open_umbrella:";
            Content = "it's raining :cat:s and :dog:s!";
        }
    }
}