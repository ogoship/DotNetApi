using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OgoShip.Models.WebApi.V1
{
    public abstract class Order
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
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        [Description("Required for new order. If given when updating order then all order lines will be replaced with the ones sent with update.")]
        public List<OrderLine> OrderLines { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Description("Write any additional comments about order.")]
        public string Comments { get; set; }

        public decimal? PriceTotal { get; set; }

        [Description("Currency of price. (ISO 4217 Code).")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string PriceCurrency { get; set; }

        [Description("Required for cash on delivery orders.")]
        public CashOnDelivery CashOnDelivery { get; set; }

        [Description("Set to true for testing purposes.")]
        public bool? Test { get; set; }
    }
}
