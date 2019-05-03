using OGOship;
using OGOship.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreOauth2Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var api = new OgoShipApi();

            var success = api.Authenticate(
                "{merchantId}",
                "{secredToken}",
                "123456",
                "abcdef",
                "read:order read:product write:product write:order");

            if (success)
            {
                Console.WriteLine("Create new testproduct");
                var product1 = new Product
                {
                    Code = DateTime.UtcNow.ToString(),
                    Name = "Testproduct",
                    LanguageCode = "en",
                };

                var productResponse = (Product)api.AddProduct(product1);

                Console.WriteLine("Rename and update product info");
                var oldCode = productResponse.Code;
                product1 = productResponse;
                product1.Code = $"{productResponse.Code}new"; // Change SKU
                product1.Name = "Test Product";
                product1.Description = "Just test product";
                product1.EanCode = "1234567890";
                product1.Group = "Test products";
                product1.InfoUrl = $"https://myshop.com/product/{product1.Code}";
                product1.PictureUrl = $"https://myshop.com/product/{product1.Code}/img1.jpg";

                product1.CountryOfOrigin = "cn";
                product1.CurrencyCode = "EUR";
                product1.SupplyPrice = 1.25M;
                product1.Supplier = "Test supplier";
                product1.SupplierCode = $"su{product1.Code}";

                productResponse = api.UpdateProduct(productResponse, oldCode);

                var order1 = new Order
                {
                    Reference = $"test{DateTime.UtcNow.ToString()}",
                    Customer = new Customer
                    {
                        Address1 = "Teknobulevardi 3-5",
                        City = "Vantaa",
                        Zip = "01530",
                        CountryCode = "fi",
                        Name = "Test Person",
                        Company = "OGOship",
                        Email = "foo@bar.com",
                        Phone = "+35810123456"
                    },
                    OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Code = product1.Code,
                        Quantity = 10
                    }
                },
                    PriceCurrency = "EUR",
                    ShippingCode = "Test Deliverytype",
                    Test = true
                };

                var response = api.AddOrder(order1);

                Console.WriteLine("Get all orders");
                var orders = api.GetOrders(modifiedAfter: new DateTime(2017, 1, 1));
                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Reference}");
                }

                Console.WriteLine("Get all products by list");
                var products = api.GetProducts();
                foreach (var product in products)
                {
                    Console.WriteLine($"Check  {product.Code}");

                    // Get product and stock by code
                    var p = api.GetProducts(code: product.Code).FirstOrDefault();
                    var s = api.GetStockLevels(productCode: product.Code).FirstOrDefault();

                    if (p == null || p.Name != product.Name || s == null || s.StockAvailable != p.StockAvailable)
                    {
                        Console.WriteLine($"Problem with {product.Code}");
                    }
                    else
                    {
                        Console.WriteLine($"Product ok {product.Code}");
                    }
                }
            }
            else
            {
                Console.WriteLine("There was error authenticating to OGOship. Please verify your login information");
                Console.WriteLine($"ClientId:     {api.ClientId}");
                Console.WriteLine($"ClientSecred: {api.ClientSecred}");
            }
            Console.WriteLine("Bye now!");
            Console.ReadLine();
        }


        private static void TestWebhooks(OgoShipApi api)
        {
            var webHooksList = api.GetWebHooks();
            var newHook = new WebHook()
            {
                Url = "test",
                Key = "testKey",
                Type = WebhookType.OrderShipped
            };

            var createdWebhook = api.CreateWebHook(newHook);
            var webhook = api.GetWebHook(createdWebhook.Id);
            api.DeleteWebHook(webhook.Id);
        }
    }
}