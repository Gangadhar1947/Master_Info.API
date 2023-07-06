using Couchbase.KeyValue;
using Couchbase;

namespace Master_Info.API.Data
{
    public interface ITeamInfoContext
    {
        ICluster Cluster { get; }
        IBucket MasterDataBucket { get; }
        ICouchbaseCollection PersonsCollection { get; }
    }
}
