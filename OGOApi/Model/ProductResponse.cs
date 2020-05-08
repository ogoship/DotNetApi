using System;
using System.ComponentModel;

namespace OgoShip.Models.WebApi.V1
{
    public class ProductResponse : Product
    {
        [ReadOnly(true)]
        [Description("Internal Id")]
        public Guid Id { get; set; }

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
}