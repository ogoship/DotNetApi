using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OGOship.Model
{
    public enum OrderStatus
    {
        Draft,
        Reserved,
        New,
        Locked,
        Collecting,
        Pending,
        Cancelled,
        Shipped,
        Returned
    }
    public class OrderSearchRules : SearchRules
    {
        public string Reference { get; set; }

        public DateTime? ModifiedAfter { get; set; }
    }
    public class OrderResponse : Order
    {
        [ReadOnly(true)]
        [Description("Warehouse will assign tracking number when available.")]
        public string TrackingNumber { get; set; }

        [ReadOnly(true)]
        [Description("Comments from OGOship.")]
        public string WarehouseComments { get; set; }

        [ReadOnly(true)]
        [Description("Latest order change.")]
        public DateTime EditTime { get; set; }
    }

    public class Order
    {
        [Required]
        [Description("Reference number of order")]
        public string Reference { get; set; }

        [Description("Code of shipping method which you have enabled at Edit Merchant page. This is required if more than 1 shipping method is selected.")]
        public string ShippingCode { get; set; }

        [Description("Code of pickup point.")]
        public string PickUpPointCode { get; set; }

        [Description("Status of order")]
        [EnumDataType(typeof(OrderStatus))]
        //[JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        [Description("Required for new order. If given when updating order then all order lines will be replaced with the ones sent with update.")]
        public List<OrderLine> OrderLines { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Description("Write any additional comments about order.")]
        public string Comments { get; set; }

        [Description("If given when updating order then all documents will be replaced with the ones sent with update.")]
        public List<Document> Documents { get; set; }

        public decimal? PriceTotal { get; set; }

        [Description("Currency of price. (ISO 4217 Code).")]
        public string PriceCurrency { get; set; }

        [Description("Required for cash on delivery orders.")]
        public CashOnDelivery CashOnDelivery { get; set; }

        [Description("Set to true for testing purposes.")]
        public bool? Test { get; set; }
    }

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

    public class CashOnDelivery
    {
        [Required]
        [Description("Amount requested from customer.")]
        public decimal Amount { get; set; }

        [StringLength(maximumLength: 3, MinimumLength = 3)]
        [Description("Currency of amount. (ISO 4217 Code). Merchant default value will be used if not specified.")]
        public string CurrencyCode { get; set; }

        [Description("Bank reference (Order reference number + required validation digits are used if not specified).")]
        public string Reference { get; set; }
    }

    public class Customer
    {
        [Required]
        public string Name { get; set; }

        public string Company { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        [Description("Use two-letter codes: ISO 3166-1 alpha-2")]
        public string CountryCode { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }

    public class Document
    {
        [Required]
        [Description("Name of type of document, e.g. \"receipt\". Documents with type \"receipt\" will be automatically printed and attached to all deliveries. (This can be changed).")]
        public string Type { get; set; }

        [Required]
        [Description("Full url of document.")]
        public string Url { get; set; }
    }

}
