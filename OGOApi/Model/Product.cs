using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
    public class Product
    {
        [Description("Display name of product.")]
        public string Name { get; set; }

        [Description("Additional information about product.")]
        public string Description { get; set; }

        [Required]
        [Description("Unique product code.")]
        public string Code { get; set; }

        [Description("Supplier given code of this product.")]
        public string SupplierCode { get; set; }

        [Description("Name of supplier.")]
        public string Supplier { get; set; }

        [Description("Group of product.")]
        public string Group { get; set; }

        [Description("EAN code of product. Existing EAN code will not be overwritten.")]
        public string EanCode { get; set; }

        //[Description("Merchant can receive reports if stock is below this alarm level.")]
        //public int? AlarmLevel { get; set; }

        [Description("Supply price of product for calculating value of stock.")]
        public decimal? SupplyPrice { get; set; }

        [Description("Sales price of product.")]
        public decimal? SalesPrice { get; set; }

        [Description("Currency of supply price. (ISO 4217 Code).")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string CurrencyCode { get; set; }

        [Description("Url of product page at webstore. This helps warehouse staff to recognize products.")]
        public string InfoUrl { get; set; }

        [Description("Url of product picture at webstore. This helps warehouse staff to recognize products.")]
        public string PictureUrl { get; set; }

        [StringLength(maximumLength: 2, MinimumLength = 2)]
        [Description("Country of origin for customs info. Use two-letter codes: ISO 3166-1 alpha-2. If not set, sending country will be used.")]
        public string CountryOfOrigin { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        [Description("Language code for product info text. Use two-letter codes: ISO 639-1. Warehouses have supported / valid languages: TLL: fi, en. GOT: en, sv. VNT: fi, en")]
        //[EnumDataType(typeof(LanguageCode))]
        //[JsonConverter(typeof(StringEnumConverter))]
        public string LanguageCode { get; set; }

        [Description("Info text for customs documents.")]
        public string CustomsDescription { get; set; }

        [Description("HSCODE of product.")]
        public string HsCode { get; set; }

    }
}