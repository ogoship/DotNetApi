using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
    public class OrderLine
    {
        [Required]
        [Description("Code of product.")]
        public string Code { get; set; }

        [Required]
        [Description("Quantity of products.")]
        public int Quantity { get; set; }

        [Description("Sales price of single product. (Price including VAT).")]
        public decimal? UnitPrice { get; set; }

        [Description("Percentage of VAT included in sales price (UnitPrice).")]
        public decimal? VatPercentage { get; set; }

        [Description("Full url of product info page. If there are lots of similar products then warehouse staff can use this page to verify products before shipping.")]
        public string ProductInfoUrl { get; set; }
 
        [Description("Full url of product picture. If there are lots of similar products then warehouse staff can use this page to verify products before shipping.")]
        public string ProductPictureUrl { get; set; }
    }
}