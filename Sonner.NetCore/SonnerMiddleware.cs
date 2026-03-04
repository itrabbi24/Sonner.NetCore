using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Sonner.NetCore
{
    public class SonnerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _cssContent;
    private readonly string _jsContent;

    public SonnerMiddleware(RequestDelegate next)
    {
        _next = next;
        
        var assembly = typeof(SonnerMiddleware).GetTypeInfo().Assembly;
        
        using (var cssStream = assembly.GetManifestResourceStream("Sonner.NetCore.wwwroot.css.sonner.css"))
        using (var cssReader = new System.IO.StreamReader(cssStream!))
        {
            _cssContent = cssReader.ReadToEnd();
        }

        using (var jsStream = assembly.GetManifestResourceStream("Sonner.NetCore.wwwroot.js.sonner.js"))
        using (var jsReader = new System.IO.StreamReader(jsStream!))
        {
            _jsContent = jsReader.ReadToEnd();
        }
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        if (path != null)
        {
            if (path.Equals("/sonner.css", System.StringComparison.OrdinalIgnoreCase))
            {
                context.Response.ContentType = "text/css";
                await context.Response.WriteAsync(_cssContent);
                return;
            }
            
            if (path.Equals("/sonner.js", System.StringComparison.OrdinalIgnoreCase))
            {
                context.Response.ContentType = "application/javascript";
                await context.Response.WriteAsync(_jsContent);
                return;
            }
        }

        await _next(context);
    }
}

public static class SonnerMiddlewareExtensions
{
    public static IApplicationBuilder UseSonner(this IApplicationBuilder builder)
    {
            return builder.UseMiddleware<SonnerMiddleware>();
        }
    }
}
