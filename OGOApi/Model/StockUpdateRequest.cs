using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
    public class StockUpdateRequest
    {
        [Description("Warehouse Code.")]
        public string WarehouseCode { get; set; }

        [Required]
        [Description("Products Supplier.")]
        public string Supplier { get; set; }

        [Description("Incoming containers.")]
        public short? Containers { get; set; }

        [Description("Incoming pallets.")]
        public short? Pallets { get; set; }

        [Description("Incoming parcels.")]
        public short? Parcels { get; set; }

        [Description("Shipping company name.")]
        public string DeliveredBy { get; set; }

        [Required]
        [Description("Products delivery date (best estimate)")]
        public DateTime? ReceiveDate { get; set; }

        [Description("Comments from merchant.")]
        public string MerchantComments { get; set; }

        [Description("Tracking code (if available).")]
        public List<string> TrackingCodes { get; set; }

        [Description("Comments for special cases.")]
        public string SpecialAction { get; set; }

        [Required]
        [Description("Products Reference code.")]
        public string Reference { get; set; }

        [ReadOnly(true)]
        [Description("Stock update status is set to NEW here automatically.")]
        public string Status { get; set; }

        public List<ProductUpdateRequest> Products { get; set; }

        public class ProductUpdateRequest
        {
            [Required]
            [Description("Product code.")]
            public string Code { get; set; }

            [Description("Product name.")]
            public string Name { get; set; }

            [Required]
            [Description("Quantity to be expexted.")]
            [Range(0,1000000,ErrorMessage = "Must be positive number>")]
            public int ExpectedQuantity { get; set; }

            [Description("Unit Price for calculating stock value.")]
            public decimal? SupplyPrice { get; set; }

            [Description("EAN Code.")]
            public string EANCode { get; set; }

            [Required]
            [StringLength(maximumLength: 2, MinimumLength = 2)]
            [Description("Country of origin for customs info. Use two-letter codes: ISO 3166-1 alpha-2. If not set, sending country will be used.")]
            public string CountryOfOrigin { get; set; }

            [StringLength(maximumLength: 50)]
            [Description("What the product is and what is it made of, max 50 characters.")]
            public string CustomsDescription { get; set; }
            
            [StringLength(maximumLength: 8, MinimumLength = 8)]
            [Description("2-digit customs tariff chapter number + 6-digit Harmonization code.")]
            public string HsCode { get; set; }


        }
    }
}



