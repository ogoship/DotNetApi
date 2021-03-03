using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{

    public class StockUpdateResponse
    {
        [ReadOnly(true)]
        [Description("Product status.")]
        public string Status { get; set; }

        [Description("Date when products arrived at the warehouse")]
        public DateTime? ArrivalDate { get; set; }

        [Description("Warehouse Code.")]
        public string WarehouseCode { get; set; }

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

        [Description("Products delivery date (best estimate)")]
        public DateTime? ReceiveDate { get; set; }

        [Description("Comments from merchant.")]
        public string MerchantComments { get; set; }

        [Description("Tracking code (if available).")]
        public List<string> TrackingCodes { get; set; }

        [Description("Comments for special cases.")]
        public string SpecialAction { get; set; }

        [Description("Products Reference code.")]
        public string Reference { get; set; }

        [ReadOnly(true)]
        [Description("Products OGO Reference for the supplier.")]
        public string AutoReference { get; set; }

        [Description("Comments from Warehouse about whole order.")]
        public string WarehouseComments { get; set; }

        [ReadOnly(true)]
        [Description("Time Created.")]
        public DateTime Created { get; set; }

        [ReadOnly(true)]
        [Description("Last Modified.")]
        public DateTime? Modified { get; set; }

        public List<ProductUpdateInfo> Products { get; set; }
        public class ProductUpdateInfo
        {
            
            [ReadOnly(true)]
            [Description("Product code.")]
            public string Code { get; set; }

            [ReadOnly(true)]
            [Description("Product Name.")]
            public  string Name { get; set; }

            [ReadOnly(true)]
            [Description("EAN Code.")]
            public string EANCode { get; set; }

            [ReadOnly(true)]
            [StringLength(maximumLength: 2, MinimumLength = 2)]
            [Description("Country of Origin.")]
            public string CountryOfOrigin { get; set; }

            [ReadOnly(true)]
            [Description("Good customs description contains what the product is, what is it made of and what it is used for")]
            public string CustomsDescription { get; set; }

            [ReadOnly(true)]
            [StringLength(maximumLength: 8, MinimumLength = 8)]
            [Description("2-digit customs tariff chapter number + 6-digit Harmonization code.")]
            public string HsCode { get; set; }

            [ReadOnly(true)]
            [Description("Quantity to be expexted.")]
            public int ExpectedQuantity { get; set; }

            [ReadOnly(true)]
            [Description("Unit Price for calculating stock value.")]
            public decimal? SupplyPrice { get; set; }

            [ReadOnly(true)]
            [Description("Quantity received.")]
            public int ReceivedQuantity { get; set; }

            [ReadOnly(true)]
            [Description("Comments from warehouse.")]
            public string WarehouseComments { get; set; }

        }
        //Copied from StockResponse.cs
        //public string ProductCode { get; set; }
        //[Description("Products total stock level of all warehouses.")]
        //public int Stock { get; set; }
        //[Description("Count of products available for orders.")]
        //public int StockAvailable { get; set; }
        //[Description("Count of products reserved for not shipped orders.")]
        //public int Reserved { get; set; }
        //[Description("Stock level modified.")]
        //public DateTime Modified { get; set; }
    }

    //public class StockUpdateInfoProvider
    //{
    //    public bool IsNew { get; }

    //    protected StockUpdateInfoProvider(bool isNew)
    //    {
    //        this.IsNew = isNew;
    //    }

    //    public string Status { get; }
    //    public DateTime? ReceiveDate { get; }
    //    public string Supplier { get; }
    //    public string Comments { get; }
    //    public string Reference { get; }
    //    public List<IProductInfo> Products { get; }

    //    public class ProductInfo : IProductInfo
    //    {
    //        public string Code { get; set; }
    //        public int Quantity { get; set; }
    //        public decimal? UnitPrice { get; set; }
    //        public string Comments { get; set; }
    //        public string Name { get; set; }
    //        public Guid? ProductId { get; set; }
    //    }

    //    public interface IProductInfo
    //    {
    //        string Code { get; }
    //        int Quantity { get; }
    //        decimal? UnitPrice { get; }
    //        string Comments { get; }
    //        string Name { get; }
    //        Guid? ProductId { get; }

    //    }
    //}

}



