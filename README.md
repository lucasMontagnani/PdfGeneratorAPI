# 📄 Generating PDFs for Free in .NET Core Web API

This project explores different ways to generate PDFs for free in a **C# .NET Core Web API**, testing three popular libraries:

- **QuestPDF**
- **OpenHtmlToPdf**
- **SelectHtmlToPdf**

## 🛠️ Tested Libraries

### 🚀 QuestPDF
- **Pros**: The most well-known and community-recommended library. It features an intuitive syntax using rows and columns to build PDF pages.
- **Cons**: The free license has limitations for commercial/enterprise use, making it more suitable for personal or small business projects.

### 📄 OpenHtmlToPdf & SelectHtmlToPdf
- Both rely on **Razor Views (CSHTML)** as a template structure for generating PDFs.
- They require the **Razor.Templating.Core** package to render views and transform them into HTML.
- Once the Razor View is created, it can be rendered as follows:
  ```csharp
  var html = await RazorTemplateEngine.RenderAsync("ViewName", model);
  
### 📌 OpenHtmlToPdf
- Convert HTML to PDF with:
  ```csharp
  var pdf = OpenHtmlToPdf.Pdf.From(html).SaveAs("output.pdf");
- Note: Displays a warning when used with .NET versions above 5, but works fine so far.
- Limitation: Struggles with some CSS properties, such as centering elements using display: flex.

### 📌 SelectHtmlToPdf
- Convert HTML to PDF with:
  ```csharp
  var converter = new HtmlToPdf();
  converter.ConvertHtmlString(html).Save("output.pdf");
- Limitation: The free version imposes a 5-page limit per PDF
  
### 📊 Comparison & Conclusion
As a conclusion I’d say that the SelectHtmlToPdf is the best choice if you going to us reports that has up to 5 pages, otherwise OpenHtmlToPdf will be your best option.
If you are willing to pay, QuestPDF is excellent, otherwise comes with free license restrictions.

### 🚀 Deploy
When deploying the project through a build and release pipeline, Razor View files (.cshtml) may require additional configuration to ensure they are included in the new environment.

## ✅ Steps to Configure Razor Views:

1. Verify Build Action
    - Right-click on the .cshtml file, go to Properties, and make sure Build Action is set to "Content".
2. Modify the .csproj File
    - Add the following property inside the <PropertyGroup> section:
      ```xml
      <PropertyGroup>
        ...
        ...
        <CopyRazorGenerateFilesToPublishDirectory>true</CopyRazorGenerateFilesToPublishDirectory>
      </PropertyGroup>
3. Install the Required NuGet Package
     - Run the following command to install Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation:
       ```sh
       dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
4. Enable Runtime Compilation
   - Add the following configuration to the Startup/Program.cs
     ```csharp
     builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

These steps ensure that Razor Views are properly included during deployment, preventing issues where the application cannot find .cshtml files at runtime.
Probably the first 2 will be enough to solve this problem.
