namespace Sonner.NetCore
{
    public enum ToastType
{
    Default,
    Success,
    Error,
    Warning,
    Info
}

    public class ToastMessage
    {
        public string Message { get; set; } = string.Empty;
        public ToastType Type { get; set; } = ToastType.Default;
        public string? Title { get; set; }
    }
}
