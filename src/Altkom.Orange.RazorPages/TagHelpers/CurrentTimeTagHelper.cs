using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.RazorPages.TagHelpers
{
    public class CurrentTimeTagHelper : TagHelper
    {
        public string Title { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";

            output.Content.SetHtmlContent($"{Title} <b>{DateTime.Now}</b>");
        }
    }
}
