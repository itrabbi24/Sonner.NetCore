# Sonner.NetCore

[![GitHub](https://img.shields.io/badge/GitHub-Repository-black?logo=github)](https://github.com/itrabbi24/Sonner.NetCore)

An elegant, robust, and lightweight toaster library for .NET Core MVC and Razor Pages. Inspired by the popular React library [Sonner](https://sonner.emilkowal.ski/), but built from the ground up for backend-driven .NET web applications.

![Sonner Demo](https://itrabbi24.github.io/images/sonner-demo.png) *(Preview of the Toast)*

By **ARG RABBI** - [Visit Profile](https://itrabbi24.github.io/)

---

## 🚀 Features

- **Full Positioning Support:** Choose from 6 different positions.
- **Stacking & Expanding:** Sophisticated stacking logic that expands on hover.
- **Rich Colors:** Vibrant background colors for success, error, info, and warning states.
- **Dark Theme:** Built-in support for dark mode.
- **Close Button:** Optional dismiss button for individual toasts.
- **No React/Vue required!** Pure Vanilla JS and CSS natively integrated into .NET.
- **Trigger Toasts from C#:** Seamlessly add toasts directly from your Controllers.
- **Tag Helper Integration:** Clean syntax in Razor views.

---

## 📦 Installation

**Via NuGet Package Manager Console:**
```bash
Install-Package ARG.RABBI.Sonner.NetCore
```

**Via .NET CLI:**
```bash
dotnet add package ARG.RABBI.Sonner.NetCore
```

---

## 💻 Configuration

Initialize the toaster in your `_Layout.cshtml` using the Tag Helper.

### Options Summary

| Attribute | Type | Default | Description |
| :--- | :--- | :--- | :--- |
| `position` | `Enum` | `BottomRight` | Toast container position. |
| `expand` | `bool` | `false` | Whether toasts should be expanded by default. |
| `rich-colors` | `bool` | `false` | Enable vibrant background colors. |
| `close-button` | `bool` | `false` | Show a close button on each toast. |
| `theme` | `string` | `light` | `light` or `dark`. |

### 1. Register the Middleware (Program.cs)
```csharp
app.UseRouting();
app.UseSonner(); // Serves the embedded CSS/JS
app.UseAuthorization();
```

### 2. Add Tag Helpers (_ViewImports.cshtml)
```cshtml
@addTagHelper *, Sonner.NetCore
```

### 3. Initialize in Layout (_Layout.cshtml)
Include the CSS in `<head>` and the script + tag helper before `</body>`.

```html
<head>
    <link rel="stylesheet" href="~/sonner.css" />
</head>
<body>
    @RenderBody()

    <script src="~/sonner.js"></script>
    <sonner-toaster 
        position="BottomRight" 
        expand="false" 
        rich-colors="true" 
        close-button="true" 
        theme="light">
    </sonner-toaster>
</body>
```

---

## 🎨 Positioning
Sonner.NetCore supports 6 positions out of the box:
- `TopLeft`
- `TopCenter`
- `TopRight`
- `BottomLeft`
- `BottomCenter`
- `BottomRight`

```html
<sonner-toaster position="TopCenter"></sonner-toaster>
```

---

## 🔥 Usage (Backend)

Trigger toasts directly from your Controllers using extension methods.

```csharp
public IActionResult Save()
{
    // Simple toast
    this.AddSuccessToast("Data saved successfully!");
    
    // Toast with title
    this.AddErrorToast("Please check your input.", "Validation Error");
    
    // Custom toast
    this.AddToast("Warning message", ToastType.Warning);
    
    return RedirectToAction("Index");
}
```

---

## ⚡ Usage (Frontend)

You can also trigger toasts manually from JavaScript using the global `window.sonnerInstance`.

```javascript
// Success toast
window.sonnerInstance.toast('Operation completed!', 'Success', 'Success Title');

// Error toast
window.sonnerInstance.toast('Something went wrong.', 'Error');
```

---

## 📜 Publishing Update
To publish a new version:
1. Update version in `.csproj`.
2. Run `dotnet pack -c Release`.
3. Upload `.nupkg` to NuGet.org.

---

*Crafted with ❤️ by ARG RABBI for the robust .NET ecosystem.*
