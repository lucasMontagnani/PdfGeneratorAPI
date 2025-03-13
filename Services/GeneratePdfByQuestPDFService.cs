using PdfGeneratorAPI.Abstractions;
using PdfGeneratorAPI.Models;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.PortableExecutable;

namespace PdfGeneratorAPI.Services
{
    public class GeneratePdfByQuestPDFService : IGeneratePdfByQuestPDFService
    {
        public GeneratePdfByQuestPDFService()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        public byte[] GeneratePdfExample(Invoice invoice)
        {
            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    page.Header()
                        .Row(row =>
                        {
                            row.RelativeItem()
                               .Column(column =>
                               {
                                   column.Item()
                                         .Text("COMPANY NAME")
                                         .FontFamily("Arial")
                                         .FontSize(20)
                                         .Bold();

                                   column.Item()
                                         .Text("Company Address")
                                         .FontFamily("Arial");
                               });

                            row.RelativeItem()
                               .ShowOnce()
                               .Text("INVOICE")
                               .AlignRight()
                               .FontFamily("Arial")
                               .ExtraBlack()
                               .FontSize(30);
                        });

                    page.Content()
                        .PaddingTop(50)
                        .Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                   .Column(column2 =>
                                   {
                                       column2.Item()
                                              .Text("Bill To:")
                                              .Bold();

                                       column2.Item()
                                              .Text(invoice.Client?.ClientName ?? "N/A")
                                              .FontFamily("Arial")
                                              .FontSize(15)
                                              .Bold();

                                       column2.Item()
                                              .Text(invoice.Client?.ClientAddress ?? "N/A");
                                   });

                                row.RelativeItem()
                                   .Column(column2 =>
                                   {
                                       column2.Item()
                                              .Text($"Invoice #: {invoice.InvoiceNumber}")
                                              .Bold()
                                              .AlignRight();

                                       column2.Item()
                                              .PaddingTop(2)
                                              .Text($"Date: {invoice.InvoiceDate:dd-MM-yyyy}")
                                              .AlignRight();
                                   });
                            });

                            column.Item().PaddingTop(50).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40); // No.
                                    columns.RelativeColumn();   // Description
                                    columns.ConstantColumn(60); // Quantity
                                    columns.ConstantColumn(65); // Unit Price
                                    columns.ConstantColumn(50); // Total
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Padding(4).Text("#").Bold();
                                    header.Cell().Padding(4).Text("Description").Bold();
                                    header.Cell().Padding(4).Text("Quantity").Bold().AlignRight();
                                    header.Cell().Padding(4).Text("Unit Price").Bold().AlignRight();
                                    header.Cell().Padding(4).Text("Total").Bold().AlignRight();

                                    header.Cell()
                                          .ColumnSpan(5)
                                          .PaddingVertical(5)
                                          .BorderBottom(1)
                                          .BorderColor(Colors.Black);
                                });

                                for (int i = 0; i < invoice.InvoiceItems?.Count; i++)
                                {
                                    var backgroundColor = i % 2 == 0 ? Color.FromHex("#ffffff") : Color.FromHex("#f0f0f0");

                                    InvoiceItem invoiceItem = invoice.InvoiceItems[i];
                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text((i + 1).ToString());
                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(invoiceItem.Description);
                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(invoiceItem.Quantity.ToString()).AlignRight();
                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(invoiceItem.UnitPrice.ToString("N2")).AlignRight();
                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text((invoiceItem.Quantity * invoiceItem.UnitPrice).ToString("N2")).AlignRight();
                                }

                                table.Cell()
                                     .ColumnSpan(5)
                                     .PaddingVertical(5)
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Black);

                                table.Cell().ColumnSpan(4).Text("Total").Bold().AlignRight();
                                table.Cell().Text(invoice.InvoiceItems?.Sum(x => (x.Quantity * x.UnitPrice)).ToString("N2")).Bold().AlignRight();
                            });
                        });

                    page.Footer()
                        .Column(column =>
                        {
                            column.Item()
                                  .PaddingVertical(10)
                                  .Text(text =>
                                  {
                                      text.Span("Page ");
                                      text.CurrentPageNumber();
                                      text.Span(" of ");
                                      text.TotalPages();
                                      text.AlignCenter();
                                  });
                        });
                });
            });

            //document.ShowInCompanion();

            return document.GeneratePdf();
        }
    }
}
