//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OGOship.Model
{
    public enum WebhookType
    {
        OrderShipped = 1,
        ProductStockLevelChanged = 2
    }

    public class WebHook
    {
        [Required]
        [Description("Resource url")]
        public string Url { get; set; }

        [Required]
        [Description("Secrete key for connect to url")]
        public string Key { get; set; }

        [Required]
        [Description("Type of webhook")]
        [EnumDataType(typeof(WebhookType))]
        //        [JsonConverter(typeof(StringEnumConverter))] //could effect deserialization
        public WebhookType Type { get; set; }
    }

    public class WebHookResponse : WebHook
    {
        public System.Guid Id { get; set; }
    }
}