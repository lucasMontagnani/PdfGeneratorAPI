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
