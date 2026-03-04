# Sonner.NetCore

[![GitHub](https://img.shields.io/badge/GitHub-Repository-black?logo=github)](https://github.com/itrabbi24/Sonner.NetCore)
[![NuGet](https://img.shields.io/nuget/v/ARG.RABBI.Sonner.NetCore.svg)](https://www.nuget.org/packages/ARG.RABBI.Sonner.NetCore/)

An elegant, robust, and lightweight toaster library for .NET Core MVC and Razor Pages. Inspired by the popular React library [Sonner](https://sonner.emilkowal.ski/), but built from the ground up for backend-driven .NET web applications.

![Sonner Demo](https://raw.githubusercontent.com/itrabbi24/Sonner.NetCore/main/images/sonner-demo.png) *(Preview of the Toast)*

By **ARG RABBI** - [Visit Profile](https://itrabbi24.github.io/)

---

## 🚀 Features

- **Framework Support:** Native support for .NET 5.0, 6.0, 7.0, 8.0, and .NET 9.0.
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

**NuGet Package:** [ARG.RABBI.Sonner.NetCore](https://www.nuget.org/packages/ARG.RABBI.Sonner.NetCore/)

**Via NuGet Package Manager Console:**
```bash
Install-Package ARG.RABBI.Sonner.NetCore
```

**Via .NET CLI:**
```bash
dotnet add package ARG.RABBI.Sonner.NetCore
```

---

## 💻 Setup Guide

Follow these simple steps to integrate Sonner into your .NET application.

### 1. Register the Middleware

This serves the embedded assets. Choose the guide based on your project style:

#### **Modern Hosting Style (.NET 6, 7, 8, & 9)**
Usually found in `Program.cs` (Top-level statements).
```csharp
app.UseRouting();

// Add this line after UseRouting() but before UseAuthorization()
app.UseSonner(); 

app.UseAuthorization();
```

#### **Legacy Hosting Style (.NET 5 and below)**
Usually found in `Startup.cs` (Configure method).
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    
    // Add this line
    app.UseSonner(); 

    app.UseAuthorization();
}
```

### 2. Add Tag Helpers (`_ViewImports.cshtml`)
Add the Sonner namespace to your `Views/_ViewImports.cshtml`:

```cshtml
@addTagHelper *, Sonner.NetCore
```

### 3. Initialize in Layout (`_Layout.cshtml`)
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
    
    // Toast with custom position override
    this.AddWarningToast("Attention required!", position: ToasterPosition.TopRight);
    
    // Custom toast
    this.AddToast("Info message", ToastType.Info, position: ToasterPosition.BottomLeft);
    
    return RedirectToAction("Index");
}
```

---

## ⚡ Usage (Frontend)

You can also trigger toasts manually from JavaScript using the global `window.sonnerInstance`. Per-toast position overrides the global configuration.

```javascript
// Success toast with default position
window.sonnerInstance.toast('Operation completed!', 'Success', 'Success Title');

// Error toast with specific position override
window.sonnerInstance.toast('Something went wrong.', 'Error', null, 'top-center');
```

---


---

*Crafted with ❤️ by ARG RABBI for the robust .NET ecosystem.*
