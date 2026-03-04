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

    public enum ToasterPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class ToasterOptions
    {
        public ToasterPosition Position { get; set; } = ToasterPosition.BottomRight;
        public bool Expand { get; set; } = false;
        public bool RichColors { get; set; } = false;
        public bool CloseButton { get; set; } = false;
        public string Theme { get; set; } = "light"; // light, dark
    }

    public class ToastMessage
    {
        public string Message { get; set; } = string.Empty;
        public ToastType Type { get; set; } = ToastType.Default;
        public string? Title { get; set; }
        public ToasterPosition? Position { get; set; }
    }
}
