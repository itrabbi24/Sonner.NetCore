namespace Sonner.NetCore
{
    /// <summary>
    /// Defines the visual style and semantic meaning of a toast notification.
    /// </summary>
    public enum ToastType
    {
        Default,
        Success,
        Error,
        Warning,
        Info
    }

    /// <summary>
    /// Supported positions for the toast container on the screen.
    /// </summary>
    public enum ToasterPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    /// <summary>
    /// Global options for the Sonner Toaster.
    /// </summary>
    public class ToasterOptions
    {
        /// <summary>
        /// Gets or sets the default position for all toasts.
        /// </summary>
        public ToasterPosition Position { get; set; } = ToasterPosition.BottomRight;

        /// <summary>
        /// Gets or sets whether toasts should be expanded by default (not stacked).
        /// </summary>
        public bool Expand { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to use vibrant background colors for Success, Error, etc.
        /// </summary>
        public bool RichColors { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to display a close button on each toast.
        /// </summary>
        public bool CloseButton { get; set; } = false;

        /// <summary>
        /// Gets or sets the UI theme ('light' or 'dark').
        /// </summary>
        public string Theme { get; set; } = "light";
    }

    /// <summary>
    /// Represents an individual toast message.
    /// </summary>
    public class ToastMessage
    {
        /// <summary>
        /// The main message text.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The semantic type of the toast.
        /// </summary>
        public ToastType Type { get; set; } = ToastType.Default;

        /// <summary>
        /// Optional bold title for the toast.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Optional per-toast position override.
        /// </summary>
        public ToasterPosition? Position { get; set; }
    }
}
