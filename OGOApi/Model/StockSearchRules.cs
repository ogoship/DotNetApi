using System;

namespace OgoShip.Models.WebApi.V1
{
    public class StockSearchRules : SearchRules
    {
        public string ProductCode { get; set; }

        public DateTime? ModifiedAfter { get; set; }
    }
}