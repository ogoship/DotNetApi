using System;
using System.ComponentModel;

namespace OGOship.Model
{
    public class StockResponse
    {
        public string ProductCode { get; set; }
        [Description("Products total stock level of all warehouses.")]
        public int Stock { get; set; }
        [Description("Count of products available for orders.")]
        public int StockAvailable { get; set; }
        [Description("Count of products reserved for not shipped orders.")]
        public int Reserved { get; set; }
        [Description("Stock level modified.")]
        public DateTime Modified { get; set; }
    }

    public class StockSearchRules : SearchRules
    {
        public string ProductCode { get; set; }

        public DateTime? ModifiedAfter { get; set; }
    }
}
