using PdfGeneratorAPI.Models;

namespace PdfGeneratorAPI.Abstractions
{
    public interface IGeneratePdfByQuestPDFService
    {
        public byte[] GeneratePdfExample(Invoice invoice);
    }
}
