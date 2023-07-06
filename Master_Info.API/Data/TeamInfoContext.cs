using Couchbase.KeyValue;
using Couchbase;
using static Master_Info.API.Data.TeamInfoContext;
using System.Linq;

namespace Master_Info.API.Data
{
   
        public static class StringExtension
        {
            public static string DefaultIfEmpty(this string str, string defaultValue)
                => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
        }

        public class TeamInfoContext : ITeamInfoContext
        {
            public ICluster Cluster { get; private set; }
            public IBucket MasterDataBucket { get; private set; }
            public ICouchbaseCollection PersonsCollection { get; private set; }

            public TeamInfoContext()
            {
                // TODO: get these variables via DI, possibly overriding config in appsettings.json
#pragma warning disable CS8604 // Possible null reference argument.
                var CB_HOST = Environment.GetEnvironmentVariable("CB_HOST").DefaultIfEmpty("localhost");
#pragma warning disable CS8604 // Possible null reference argument.
                var CB_USER = Environment.GetEnvironmentVariable("CB_USER").DefaultIfEmpty("services");
#pragma warning disable CS8604 // Possible null reference argument.
                var CB_PSWD = Environment.GetEnvironmentVariable("CB_PSWD").DefaultIfEmpty("BSHtool2019!");
#pragma warning restore CS8604 // Possible null reference argument.

                Console.WriteLine(
                    $"Connecting to couchbase://{CB_HOST} with {CB_USER} / {CB_PSWD}");

                try
                {
                    var task = Task.Run(async () => {
                        var cluster = await Couchbase.Cluster.ConnectAsync(
                            CB_HOST,
                            CB_USER,
                            CB_PSWD);
                        Cluster = cluster;
                        MasterDataBucket = await Cluster.BucketAsync("master-data");
                        var inventoryScope = await MasterDataBucket.ScopeAsync("inventory");
                        PersonsCollection = await inventoryScope.CollectionAsync("persons");
                    });
                    task.Wait();
                }
                catch (AggregateException ae)
                {
                    ae.Handle((x) => throw x);
                }
            }


        }

    
}
