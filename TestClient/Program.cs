using OGOship;
using OGOship.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using OgoShip.Models.WebApi.V1;

namespace CoreOauth2Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var api = new OgoShipApi();
            var merchantId = "{merchantId}";
            var secredToken = "{secredToken}";
            var applicationId = "123456";
            var applicationSecret = "abcdef";

            var success = api.Authenticate(
                merchantId,
                secredToken,
                applicationId,
                applicationSecret,
                "read:order write:order read:product write:product read:webhook write:webhook read:metadata write:metadata");

            if (success)
            {
                Console.WriteLine("Authentication done.");

                //WorkWithWebHooks(api);
                GetProductsAndOrders(api);
                PostStockUpdate(api);
                Console.ReadKey();
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

        private static void GetProductsAndOrders(OgoShipApi api)
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

            var order1 = new OrderRequest
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


        private static void PostStockUpdate(OgoShipApi api)
        {


            Console.WriteLine("Create New StockUpdate");
            var stockUpdate1 = new StockUpdateRequest
            {
                WarehouseCode = "TEST",
                Status = "",
                Supplier = "Test supplier",
                Containers = 1,
                Pallets = 10,
                Parcels = 100,
                DeliveredBy = "Test Supplier",
                ReceiveDate = DateTime.UtcNow.AddDays(5),
                MerchantComments = "Test Merchant Comment",
                TrackingCodes = new List<string> { "123456789", "987654321" },
                SpecialAction = "special action comment if needed",
                Reference = "testreference",
                Products = new List<StockUpdateRequest.ProductUpdateRequest>
                {
                    new StockUpdateRequest.ProductUpdateRequest
                    {
                        Code = "TestProductCode",
                        Name = "Test Product",
                        ExpectedQuantity = 100,
                        SupplyPrice = (decimal?)10.99,
                        EANCode = "1234567890",
                        CountryOfOrigin = "FI",
                        CustomsDescription = "Test product made out of test",
                        HsCode = "20111222"
                    }
                }
            };

            api.AddStockUpdate(stockUpdate1);



            Console.WriteLine("Get stock update by reference");
            var stockUpdates = api.GetStockUpdate(null, stockUpdate1.Reference, null);
            foreach (var stockUpdate in stockUpdates)
            {                  
                Console.WriteLine($"Reference: {stockUpdate.Reference}");                
            }
            


        }
        private static void WorkWithWebHooks(OgoShipApi api)
        {
            List<WebHookResponse> webHooksList = api.GetWebHooks();

            var newHook = new WebHook()
            {
                Url = "test",
                Key = "testKey",
                Type = WebhookType.OrderShipped
            };

            WebHookResponse createdWebhook = api.CreateWebHook(newHook);

            WebHookResponse webhook = api.GetWebHook(createdWebhook.Id);

            webhook.Key = "TestTestTestKey";
            WebHookResponse  updatedWebhook = api.UpdateWebHook(createdWebhook.Id, webhook);

            webHooksList = api.GetWebHooks();

            api.DeleteWebHook(webhook.Id);

            webHooksList = api.GetWebHooks();
        }
    }
}