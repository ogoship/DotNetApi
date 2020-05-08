using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OgoShip.Models.WebApi.V1
{
    public class OrderResponse : Order
    {
        [ReadOnly(true)]
        [Description("Internal Id")]
        public Guid Id { get; set; }

        [ReadOnly(true)]
        [Description("Warehouse will assign tracking number when available.")]
        public string TrackingNumber { get; set; }

        [ReadOnly(true)]
        [Description("Warehouse will assign tracking url when available.")]
        public string TrackingUrl { get; set; }

        [ReadOnly(true)]
        [Description("Comments from OGOship.")]
        public string WarehouseComments { get; set; }

        [ReadOnly(true)]
        [Description("Latest order change.")]
        public DateTime EditTime { get; set; }

        [Description("If given when updating order then all documents will be replaced with the ones sent with update.")]
        public List<DocumentResponse> Documents { get; set; }

    }
}