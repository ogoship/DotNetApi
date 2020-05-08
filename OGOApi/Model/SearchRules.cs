using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OgoShip.Models.WebApi.V1
{
    public class SearchRules
    {
        [DefaultValue(50)]
        [Range(1, 250)]
        [Description("The maximum number of results to show on a page.")]
        public int Limit { get; set; } = 50;
        [DefaultValue(1)]
        [Description("The page of results to show.")]
        public int Page { get; set; } = 1;
    }
}
