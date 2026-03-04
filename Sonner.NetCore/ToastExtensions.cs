using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Collections.Generic;

namespace Sonner.NetCore
{
    /// <summary>
    /// Extensions for Microsoft.AspNetCore.Mvc.Controller to easily add Sonner toasts.
    /// </summary>
    public static class ToastExtensions
    {
        private const string TempDataKey = "SonnerToasts";

        /// <summary>
        /// Adds a generic toast notification to the current request.
        /// </summary>
        public static void AddToast(this Controller controller, string message, ToastType type = ToastType.Default, string? title = null, ToasterPosition? position = null)
        {
            var tempDataProvider = controller.TempData;
            var toasts = GetToasts(tempDataProvider);

            toasts.Add(new ToastMessage { Message = message, Type = type, Title = title, Position = position });

            tempDataProvider[TempDataKey] = JsonSerializer.Serialize(toasts);
        }

        /// <summary>
        /// Adds a success (green) toast notification.
        /// </summary>
        public static void AddSuccessToast(this Controller controller, string message, string? title = null, ToasterPosition? position = null)
            => AddToast(controller, message, ToastType.Success, title, position);

        /// <summary>
        /// Adds an error (red) toast notification.
        /// </summary>
        public static void AddErrorToast(this Controller controller, string message, string? title = null, ToasterPosition? position = null)
            => AddToast(controller, message, ToastType.Error, title, position);

        /// <summary>
        /// Adds a warning (yellow) toast notification.
        /// </summary>
        public static void AddWarningToast(this Controller controller, string message, string? title = null, ToasterPosition? position = null)
            => AddToast(controller, message, ToastType.Warning, title, position);

        /// <summary>
        /// Adds an informational (blue) toast notification.
        /// </summary>
        public static void AddInfoToast(this Controller controller, string message, string? title = null, ToasterPosition? position = null)
            => AddToast(controller, message, ToastType.Info, title, position);

        /// <summary>
        /// Internal method to retrieve queued toasts from TempData.
        /// </summary>
        public static List<ToastMessage> GetToasts(ITempDataDictionary tempData)
        {
            if (tempData.TryGetValue(TempDataKey, out var val) && val is string json)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<ToastMessage>>(json, options) ?? new List<ToastMessage>();
            }
            return new List<ToastMessage>();
        }
    }
}
