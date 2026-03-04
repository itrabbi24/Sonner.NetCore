using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Json;
using System.Text;

namespace Sonner.NetCore
{
    [HtmlTargetElement("sonner-toaster")]
    public class SonnerToasterTagHelper : TagHelper
{
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null; // Do not render the <sonner-toaster> tag itself

        var toasts = ToastExtensions.GetToasts(ViewContext.TempData);
        if (toasts.Count == 0) return;

        var scriptBuilder = new StringBuilder();
        scriptBuilder.AppendLine("<script>");
        scriptBuilder.AppendLine("document.addEventListener('DOMContentLoaded', function() {");

        foreach (var toast in toasts)
        {
            var message = JsonSerializer.Serialize(toast.Message);
            var title = toast.Title != null ? JsonSerializer.Serialize(toast.Title) : "null";
            scriptBuilder.AppendLine($"    window.sonner.toast({message}, '{toast.Type}', {title});");
        }

        scriptBuilder.AppendLine("});");
        scriptBuilder.AppendLine("</script>");

            output.Content.SetHtmlContent(scriptBuilder.ToString());
        }
    }
}
