using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Json;

namespace Sonner.NetCore
{
    /// <summary>
    /// Tag Helper to render the Sonner toaster and initialize it with queued toasts from TempData.
    /// </summary>
    [HtmlTargetElement("sonner-toaster")]
    public class SonnerToasterTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        /// <summary>
        /// Gets or sets the default position for toasts (e.g., BottomRight, TopCenter).
        /// </summary>
        public ToasterPosition Position { get; set; } = ToasterPosition.BottomRight;

        /// <summary>
        /// Gets or sets whether toasts should be expanded by default.
        /// </summary>
        public bool Expand { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to use vibrant background colors.
        /// </summary>
        public bool RichColors { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to show a close button on each toast.
        /// </summary>
        public bool CloseButton { get; set; } = false;

        /// <summary>
        /// Gets or sets the UI theme ('light' or 'dark').
        /// </summary>
        public string Theme { get; set; } = "light";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Don't render the <sonner-toaster> tag itself

            var toasts = ToastExtensions.GetToasts(ViewContext.TempData);
            
            var scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine("<script>");
            scriptBuilder.AppendLine("document.addEventListener('DOMContentLoaded', function() {");

            // Map position enum to JS string (TopLeft -> top-left)
            string pos = Position.ToString();
            if (pos.StartsWith("Top")) pos = "top-" + pos.Substring(3).ToLower();
            else if (pos.StartsWith("Bottom")) pos = "bottom-" + pos.Substring(6).ToLower();

            // Initialize global instance with options
            scriptBuilder.AppendLine($"    if (!window.sonnerInstance) {{ ");
            scriptBuilder.AppendLine($"        window.sonnerInstance = new SonnerToaster({{ ");
            scriptBuilder.AppendLine($"            position: '{pos.ToLower()}', ");
            scriptBuilder.AppendLine($"            expand: {Expand.ToString().ToLower()}, ");
            scriptBuilder.AppendLine($"            richColors: {RichColors.ToString().ToLower()}, ");
            scriptBuilder.AppendLine($"            closeButton: {CloseButton.ToString().ToLower()}, ");
            scriptBuilder.AppendLine($"            theme: '{Theme}' ");
            scriptBuilder.AppendLine($"        }}); ");
            scriptBuilder.AppendLine($"    }}");

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
