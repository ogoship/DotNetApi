using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OgoShip.Models.WebApi.V1
{
    public class ObjectMetadata
    {
        [Description("Id of object")]
        public Guid Id { get; set; }

        [Description("Metadata of object")]
        public Dictionary<string,string> Metadata { get; set; }
    }

}