# Sonner.NetCore

[![GitHub](https://img.shields.io/badge/GitHub-Repository-black?logo=github)](https://github.com/itrabbi24/Sonner.NetCore)

An elegant, robust, and lightweight toaster library for .NET Core MVC and Razor Pages. Inspired by the popular React library [Sonner](https://sonner.emilkowal.ski/), but built from the ground up for backend-driven .NET web applications.

![Sonner Demo](https://itrabbi24.github.io/images/sonner-demo.png) *(Preview of the Toast)*

By **ARG RABBI** - [Visit Profile](https://itrabbi24.github.io/)

---

## 🚀 Features

- **No React/Vue required!** Pure Vanilla JS and CSS natively integrated into .NET.
- **Trigger Toasts from C#:** Seamlessly add toasts directly from your Controllers (`.AddSuccessToast()`).
- **Tag Helper Integration:** Simply drop `<sonner-toaster></sonner-toaster>` into your Layout.
- **Embedded Resources:** CSS and JS are bundled directly inside the NuGet package. No manual copying needed.
- **Framework Support:** Compatible with .NET 5.0 through .NET 9.0.
- **Modern UI:** Smooth animations, clean aesthetics, and responsive design.

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

*(Note: Ensure you are publishing to NuGet.org under the ID "ARG.RABBI.Sonner.NetCore" for these commands to work).*

---

## 💻 Setup Guide

Follow these 3 simple steps to integrate Sonner into your .NET Core application.

### 1. Register the Middleware (Program.cs / Startup.cs)
This middleware serves the beautifully crafted Sonner CSS and JS files that come embedded in the package.

**For .NET 6+ (`Program.cs`):**
```csharp
app.UseRouting();

// Add this line after UseRouting() but before UseAuthorization()
app.UseSonner(); 

app.UseAuthorization();
```

**For .NET 5 (`Startup.cs`):**
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    
    app.UseSonner(); // Serves the embedded CSS/JS

    app.UseAuthorization();
}
```

### 2. Add the Tag Helpers (`_ViewImports.cshtml`)
Open your `Views/_ViewImports.cshtml` file and add the Sonner namespace so your views can recognize the custom tag:

```cshtml
@using YourAppName
@using YourAppName.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers

<!-- Add this line -->
@addTagHelper *, Sonner.NetCore
```

### 3. Initialize the Toaster (`_Layout.cshtml`)
Open your `Views/Shared/_Layout.cshtml`. You need to include the CSS in your `<head>`, and place the JS and `<sonner-toaster>` tag right before the closing `</body>` tag.

```html
<head>
    <!-- ... other links ... -->
    <link rel="stylesheet" href="~/sonner.css" />
</head>
<body>
    <!-- ... your app content ... -->

    <!-- Load Sonner -->
    <script src="~/sonner.js"></script>
    <sonner-toaster></sonner-toaster>
</body>
</html>
```

---

## 🔥 Usage (Triggering Toasts from C#)

The magic of this package is how easy it is to trigger a beautiful UI toast from your backend server logic.

In any of your Controllers, you can use the built-in extension methods before returning a View or Redirect:

```csharp
using Microsoft.AspNetCore.Mvc;
using Sonner.NetCore; // Import the extensions

public class AccountController : Controller
{
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (model.IsValid)
        {
            // Trigger a Success Toast!
            this.AddSuccessToast("Welcome back, Mr. Rabbi!", "Login Successful");
            return RedirectToAction("Dashboard");
        }
        else
        {
            // Trigger an Error Toast!
            this.AddErrorToast("Invalid credentials provided.", "Login Failed");
            return View(model);
        }
    }
    
    public IActionResult SaveSettings()
    {
        // General Toast with custom type
        this.AddToast("Your settings have been updated.", ToastType.Info, "Settings Saved");
        return RedirectToAction("Index");
    }
}
```

### Supported Toast Types
- `ToastType.Default`
- `ToastType.Success`
- `ToastType.Error`
- `ToastType.Warning`
- `ToastType.Info`

---

## 🛠️ How it Works under the Hood

1. **TempData Storage:** When you call `this.AddSuccessToast()`, the message is serialized into JSON and stored securely in ASP.NET's `TempData` dictionary.
2. **TagHelper Rendering:** Upon the next page load (even across redirects), the `<sonner-toaster>` Tag Helper reads the TempData.
3. **JS Execution:** The Tag Helper injects lightweight Vanilla JavaScript to trigger the beautiful `window.sonner.toast()` animation on the user's screen.

---

## 📜 Publishing to NuGet (For the Developer)

If you are modifying this library and want to publish an update to NuGet:

1. Open `Sonner.NetCore/Sonner.NetCore.csproj`.
2. Update the `<Version>1.0.1</Version>` tag.
3. Open your terminal at the solution root and run:
   ```bash
   cd Sonner.NetCore
   dotnet pack -c Release
   ```
4. Find the `.nupkg` file in `bin/Release/`.
5. Upload to [NuGet.org](https://www.nuget.org/).

---

*Crafted with ❤️ by ARG RABBI for the robust .NET ecosystem.*
