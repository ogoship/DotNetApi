using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OGOship;
using OGOship.Model;

namespace CoreOauth2Client
{



class Program
{
    static void Main(string[] args)
    {
        var api = new OgoShipApi();

        var success = api.Authenticate(
            "{merchantId}",
            "{secredToken}",
            "123456",
            "abcdef", "read:order read:product write:product");

        if (success)
        {
            var p1 = new Product
            {
                Code = DateTime.UtcNow.ToString(),
                Name = "Testproduct",
                LanguageCode = "en"
            };

            var p2 = api.AddProduct(p1);
            p2.Name = "Test Product";

            var p3 = api.UpdateProduct(p2);

                //Console.WriteLine($"Wait for {61} sec to expire.");
                //Task.Delay(61 * 1000).Wait();
                //Console.WriteLine($"Done waiting.");

            var orders = api.GetOrders(modifiedAfter: new DateTime(2017, 1, 1));

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Reference}");
            }

            var products = api.GetProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"Check {product.Code}");


                var p = api.GetProducts(code:product.Code).FirstOrDefault();
                var s = api.GetStockLevels(productCode:product.Code).FirstOrDefault();

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

        Console.WriteLine("Hello World!");
        Console.ReadLine();
    }
}
}
