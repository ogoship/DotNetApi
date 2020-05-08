using System;

namespace OgoShip.Models.WebApi.V1
{
    public class ProductSearchRules : SearchRules
    {
        public string Code { get; set; }
        public string EanCode { get; set; }
        public DateTime? ModifiedAfter { get; set; }
    }
}