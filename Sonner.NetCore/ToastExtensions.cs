using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Collections.Generic;

namespace Sonner.NetCore
{
    public static class ToastExtensions
{
    private const string TempDataKey = "SonnerToasts";

    public static void AddToast(this Controller controller, string message, ToastType type = ToastType.Default, string? title = null)
    {
        var tempDataProvider = controller.TempData;
        var toasts = GetToasts(tempDataProvider);

        toasts.Add(new ToastMessage { Message = message, Type = type, Title = title });

        tempDataProvider[TempDataKey] = JsonSerializer.Serialize(toasts);
    }

    public static void AddSuccessToast(this Controller controller, string message, string? title = null)
        => AddToast(controller, message, ToastType.Success, title);

    public static void AddErrorToast(this Controller controller, string message, string? title = null)
        => AddToast(controller, message, ToastType.Error, title);

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
