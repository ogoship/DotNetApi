using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OgoShip.Models.WebApi.V1
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageCode
    {
        [EnumMember(Value = "fi")]
        Fi,
        [EnumMember(Value = "en")]
        En,
        [EnumMember(Value = "sv")]
        Sv
    }
}