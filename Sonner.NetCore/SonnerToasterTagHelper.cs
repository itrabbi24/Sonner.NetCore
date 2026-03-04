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

        public ToasterPosition Position { get; set; } = ToasterPosition.BottomRight;
        public bool Expand { get; set; } = false;
        public bool RichColors { get; set; } = false;
        public bool CloseButton { get; set; } = false;
        public string Theme { get; set; } = "light";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            var toasts = ToastExtensions.GetToasts(ViewContext.TempData);
            
            var scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine("<script>");
            scriptBuilder.AppendLine("document.addEventListener('DOMContentLoaded', function() {");

            // Initialize toaster with options
            var options = new
            {
                position = Position.ToString().ToLower().Replace("bottom", "bottom-").Replace("top", "top-").Replace("center", "center").Replace("left", "left").Replace("right", "right"),
                expand = Expand,
                richColors = RichColors,
                closeButton = CloseButton,
                theme = Theme
            };
            
            // Fix position mapping (TopLeft -> top-left, etc)
            string pos = Position.ToString();
            if (pos.StartsWith("Top")) pos = "top-" + pos.Substring(3).ToLower();
            else if (pos.StartsWith("Bottom")) pos = "bottom-" + pos.Substring(6).ToLower();

            scriptBuilder.AppendLine($"    if (!window.sonnerInstance) {{ window.sonnerInstance = new SonnerToaster({{ position: '{pos.ToLower()}', expand: {Expand.ToString().ToLower()}, richColors: {RichColors.ToString().ToLower()}, closeButton: {CloseButton.ToString().ToLower()}, theme: '{Theme}' }}); }}");

            foreach (var toast in toasts)
            {
                var message = JsonSerializer.Serialize(toast.Message);
                var title = toast.Title != null ? JsonSerializer.Serialize(toast.Title) : "null";
                var toastPos = "null";
                if (toast.Position.HasValue)
                {
                    string p = toast.Position.Value.ToString();
                    if (p.StartsWith("Top")) p = "top-" + p.Substring(3).ToLower();
                    else if (p.StartsWith("Bottom")) p = "bottom-" + p.Substring(6).ToLower();
                    toastPos = $"'{p.ToLower()}'";
                }
                
                scriptBuilder.AppendLine($"    window.sonnerInstance.toast({message}, '{toast.Type}', {title}, {toastPos});");
            }

            scriptBuilder.AppendLine("});");
            scriptBuilder.AppendLine("</script>");

            output.Content.SetHtmlContent(scriptBuilder.ToString());
        }
    }
}
