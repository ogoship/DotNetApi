using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
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
}