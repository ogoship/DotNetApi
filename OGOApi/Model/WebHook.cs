using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OGOship.Model;

namespace OgoShip.Models.WebApi.V1
{
    public class WebHook
    {
        [Required]
        [RegularExpression(@"^(http(s)?:\/\/)[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$",
            ErrorMessage ="Field 'Url' has not a proper url value. Url should start with http:// or https://")]
        [Description("Resource url")]
        public string Url { get; set; }

        [Required]
        [Description("Secret key for connection to url")]
        public string Key { get; set; }

        [Required]
        [Description("Type of webhook")]
        [EnumDataType(typeof(WebhookType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebhookType Type { get; set; }

        [Description("Custom headers of webhook")]
        public Dictionary<string,string> CustomHeaders { get; set; }
    }
}