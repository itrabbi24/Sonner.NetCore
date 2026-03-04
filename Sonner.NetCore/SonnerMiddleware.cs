using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Sonner.NetCore
{
    /// <summary>
    /// Middleware to serve Sonner's embedded CSS and JS files.
    /// </summary>
    public class SonnerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _cssContent;
        private readonly string _jsContent;

        public SonnerMiddleware(RequestDelegate next)
        {
            _next = next;

            var assembly = typeof(SonnerMiddleware).Assembly;

            // Load embedded CSS
            using (var cssStream = assembly.GetManifestResourceStream("Sonner.NetCore.wwwroot.css.sonner.css"))
            {
                if (cssStream == null) throw new InvalidOperationException("Could not find embedded sonner.css");
                using (var cssReader = new StreamReader(cssStream))
                {
                    _cssContent = cssReader.ReadToEnd();
                }
            }

            // Load embedded JS
            using (var jsStream = assembly.GetManifestResourceStream("Sonner.NetCore.wwwroot.js.sonner.js"))
            {
                if (jsStream == null) throw new InvalidOperationException("Could not find embedded sonner.js");
                using (var jsReader = new StreamReader(jsStream))
                {
                    _jsContent = jsReader.ReadToEnd();
                }
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (!string.IsNullOrEmpty(path))
            {
                if (path.Equals("/sonner.css", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.ContentType = "text/css";
                    await context.Response.WriteAsync(_cssContent);
                    return;
                }

                if (path.Equals("/sonner.js", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.ContentType = "application/javascript";
                    await context.Response.WriteAsync(_jsContent);
                    return;
                }
            }

            await _next(context);
        }
    }

    /// <summary>
    /// Extension methods for Sonner middleware.
    /// </summary>
    public static class SonnerMiddlewareExtensions
    {
        /// <summary>
        /// Enables serving of Sonner's embedded assets (sonner.css and sonner.js).
        /// </summary>
        public static IApplicationBuilder UseSonner(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SonnerMiddleware>();
        }
    }
}
