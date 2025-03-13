using Bogus;
using PdfGeneratorAPI.Abstractions;
using PdfGeneratorAPI.Models;
using System.Globalization;

namespace PdfGeneratorAPI.Services
{
    public class GenerateDataService : IGenerateDataService
    {

        public Invoice GenerateInvoice()
        {
            Client client = new()
            {
                ClientName = "Lucas Montagnani",
                ClientAddress = "São Paulo, SP"
            };

            InvoiceItem invoiceItem1 = new()
            {
                Description = "Some descption...",
                Quantity = 2,
                UnitPrice = 50
            };

            InvoiceItem invoiceItem2 = new()
            {
                Description = "Some other descption...",
                Quantity = 3,
                UnitPrice = 25
            };

            Invoice invoice = new()
            {
                InvoiceNumber = "123",
                InvoiceDate = DateTime.Now,
                Client = client,
                InvoiceItems = new List<InvoiceItem> { invoiceItem1, invoiceItem2 }
            };

            return invoice;
        }

        public InvoiceContract GenerateInvoiceContract()
        {
            var faker = new Faker();

            return new InvoiceContract
            {
                Number = faker.Random.Number(100_000, 1_000_000).ToString(),
                IssuedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                SellerAddress = new Address
                {
                    CompanyName = faker.Company.CompanyName(),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Email = faker.Internet.Email()
                },
                CustomerAddress = new Address
                {
                    CompanyName = faker.Company.CompanyName(),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Email = faker.Internet.Email()
                },
                LineItems = Enumerable
                    .Range(1, 100)
                    .Select(i => new LineItem
                    {
                        Id = i,
                        Name = faker.Commerce.ProductName(),
                        Price = faker.Random.Decimal(10, 1000),
                        Quantity = faker.Random.Decimal(1, 10)
                    })
                    .ToArray()
            };
        }       
    }
}
