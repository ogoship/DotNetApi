using System;

namespace OgoShip.Models.WebApi.V1
{
    public class OrderSearchRules : SearchRules
    {
        public string Reference { get; set; }

        public DateTime? ModifiedAfter { get; set; }
    }
}