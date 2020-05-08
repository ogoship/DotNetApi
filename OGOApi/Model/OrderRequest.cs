using System.Collections.Generic;
using System.ComponentModel;

namespace OgoShip.Models.WebApi.V1
{
    public class OrderRequest : Order
    {
        [Description("If given when updating order then all documents will be replaced with the ones sent with update.")]
        public List<Document> Documents { get; set; }
    }
}