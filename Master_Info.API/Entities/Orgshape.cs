using System.Text.Json.Serialization;

namespace Master_Info.API.Entities
{
    public class Shape
    {
        [JsonPropertyName("org_shapes")]
        public List<Org_Shape>? Org_Shapes { get; set; }

    }
    public class Org_Shape
    {
        [JsonPropertyName("org_shape")]
        public string? OrgShape { get; set; }
    }


}
