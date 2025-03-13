using PdfGeneratorAPI.Models;

namespace PdfGeneratorAPI.Abstractions
{
    public interface IGenerateDataService
    {
        public Invoice GenerateInvoice();
        public InvoiceContract GenerateInvoiceContract();
    }
}
