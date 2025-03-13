using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenHtmlToPdf;
using PdfGeneratorAPI.Abstractions;
using PdfGeneratorAPI.Models;
using Razor.Templating.Core;
using SelectPdf;
using System.Threading.Tasks;

namespace PdfGeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IGeneratePdfByQuestPDFService _generatePdfByQuestPDFService;
        private readonly IGenerateDataService _generateDataService;

        public PdfController(IGeneratePdfByQuestPDFService generatePdfByQuestPDFService, IGenerateDataService generateDataService)
        {
            _generatePdfByQuestPDFService = generatePdfByQuestPDFService;
            _generateDataService = generateDataService;
        }

        [HttpGet("GeneratePdf_QuestPDF")]
        public ActionResult GeneratePdf_QuestPDF()
        {
            Invoice invoice = _generateDataService.GenerateInvoice();

            byte[] document = _generatePdfByQuestPDFService.GeneratePdfExample(invoice);

            return File(document, "application/pdf", "invoice.pdf");
        }

        [HttpGet("GeneratePdf_RazorPage_OpenHtmlToPdf")]
        public async Task<ActionResult> GeneratePdf_RazorPage_OpenHtmlToPdf()
        {

            InvoiceContract invoiceContract = _generateDataService.GenerateInvoiceContract();

            string html = await RazorTemplateEngine.RenderAsync("Views/InvoiceReportStyle.cshtml", invoiceContract);

            byte[] pdf = OpenHtmlToPdf.Pdf.From(html)
                                       .OfSize(PaperSize.A4)
                                       .WithMargins(0.Centimeters())
                                       .Portrait()
                                       .Content();

            return File(pdf, "application/pdf", "invoice.pdf");
        }

        [HttpGet("GeneratePdf_RazorPage_SelectHtmlToPdf")]
        public async Task<ActionResult> GeneratePdfByRazorPageAndSelectHtmlToPdf()
        {

            InvoiceContract invoiceContract = _generateDataService.GenerateInvoiceContract();

            string html = await RazorTemplateEngine.RenderAsync("Views/InvoiceReportStyle.cshtml", invoiceContract);

            HtmlToPdf converter = new();
            PdfDocument? doc = converter.ConvertHtmlString(html);
            byte[] pdf = doc.Save();
            doc.Close();

            return File(pdf, "application/pdf", "invoice.pdf");
        }        
    }
}
