using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
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
}