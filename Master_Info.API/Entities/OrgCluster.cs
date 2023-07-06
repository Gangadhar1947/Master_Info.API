using System.Text.Json.Serialization;

namespace Master_Info.API.Entities
{
    
    public class ClusterInfo
    {
        [JsonPropertyName("org_clusters")]
        public List<Org_Cluster>? Org_Clusters { get; set; }

    }
    public class Org_Cluster
    {
        [JsonPropertyName("org_cluster")]
        public string? OrgCluster { get; set; }
    }
}
