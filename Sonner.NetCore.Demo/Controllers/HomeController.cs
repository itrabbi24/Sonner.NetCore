using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sonner.NetCore.Demo.Models;
using System.Diagnostics;

namespace Sonner.NetCore.Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TriggerToast(string type, string title, string message)
        {
            var toastType = type switch
            {
                "Success" => ToastType.Success,
                "Error" => ToastType.Error,
                "Warning" => ToastType.Warning,
                "Info" => ToastType.Info,
                _ => ToastType.Default
            };

            // This is the extension method your package provides!
            this.AddToast(message, toastType, string.IsNullOrEmpty(title) ? null : title);

            return RedirectToAction("Index");
        }
    }
}
