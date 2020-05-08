using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
    public class Document
    {
        [Required]
        [Description("Name of type of document, e.g. \"receipt\". Documents with type \"receipt\" will be automatically printed and attached to all deliveries. (This can be changed).")]
        public string Type { get; set; }

        [Description("Full url of document.")]
        public string Url { get; set; }

        [Description("Base64 encodend content of document.")]
        public string Base64Content { get; set; }
    }

    public class DocumentResponse
    {
        [Required]
        [Description("Name of type of document, e.g. \"receipt\". Documents with type \"receipt\" will be automatically printed and attached to all deliveries. (This can be changed).")]
        public string Type { get; set; }

        [Required]
        [Description("Full url of document.")]
        public string Url { get; set; }
    }
}