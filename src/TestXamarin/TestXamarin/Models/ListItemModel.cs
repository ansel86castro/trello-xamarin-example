using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace TestXamarin.Models
{
    public class ListItemModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("idList")]
        public string IdList { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
