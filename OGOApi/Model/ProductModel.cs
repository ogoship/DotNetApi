using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OGOship.Model
{
//    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageCode
    {
        [EnumMember(Value = "fi")]
        Fi,
        [EnumMember(Value = "en")]
        En,
        [EnumMember(Value = "sv")]
        Sv
    }

    public class SearchRules
    {
        [DefaultValue(50)]
        [Range(1, 250)]
        [Description("The maximum number of results to show on a page.")]
        public int Limit { get; set; } = 50;
        [DefaultValue(1)]
        [Description("The page of results to show.")]
        public int Page { get; set; } = 1;
    }

    public class ProductSearchRules : SearchRules
    {
        public string Code { get; set; }
        public string EanCode { get; set; }
        public DateTime? ModifiedAfter { get; set; }
    }

    public class ProductResponse : Product
    {
        [ReadOnly(true)]
        [Description("Count of products in stock.")]
        public int Stock { get; set; }

        [ReadOnly(true)]
        [Description("Count of products available for orders.")]
        public int StockAvailable { get; set; }

        [ReadOnly(true)]
        [Description("Count of products reserved for not shipped orders.")]
        public int Reserved { get; set; }

        [ReadOnly(true)]
        [Description("Quantity of new products coming to stock.")]
        public int StockUpdate { get; set; }

        [ReadOnly(true)]
        [Description("Date and time (ISO 8601 format) estimate of new stock update coming.")]
        public DateTime? StockUpdateTime { get; set; }

        [ReadOnly(true)]
        [Description("Date and time (ISO 8601 format) of last change made to this product.")]
        public DateTime EditTime { get; set; }
    }

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

        //[Description("Sales price of product.")]
        //public decimal? SalesPrice { get; set; }

        [Description("Currency of supply price. (ISO 4217 Code).")]
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

        //[Description("Info text for customs documents.")]
        //public string CustomsDescription { get; set; }

    }
}
