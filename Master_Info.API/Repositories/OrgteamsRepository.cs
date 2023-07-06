using Master_Info.API.Data;
using Master_Info.API.Entities;
using Couchbase;
using Couchbase.KeyValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Search;
using Couchbase.Search.Queries.Simple;
using Couchbase.Search.Queries.Compound;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Couchbase.Query;
using System.Collections;
using Couchbase.Analytics;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Master_Info.API.Repositories
{
    public class OrgteamsRepository : IOrgTeamsRepository
    {

        private readonly ITeamInfoContext _MasterInfoContext;

        public OrgteamsRepository(ITeamInfoContext MasterInfoContext)
        {
            _MasterInfoContext = MasterInfoContext ?? throw new ArgumentNullException(nameof(MasterInfoContext));
            // _bucket = EmployeeContext.MasterDataBucket;
        }


        public async Task<(IEnumerable<Team>, string[])> GetOrgTeams(string search_orgstream_Name)
        {

            var query = "SELECT org_teamslist " +
                        " FROM `master-data`.`inventory`.`org_teams`" +
                            " WHERE org_stream = $org_stream";



            var result = await _MasterInfoContext.Cluster.QueryAsync<dynamic>(query, options => options.Parameter("org_stream", search_orgstream_Name));
           // var result = await _MasterInfoContext.Cluster.QueryAsync<dynamic>("SELECT org_team FROM `master-data`.`inventory`.`org_teams` where org_stream=" + search_orgstream_Name, new Couchbase.Query.QueryOptions());

            var teams = result.Rows.Select(row =>
            {
                var org_Teams = (row.org_teamslist as JArray)?.Select(stream =>
                {
                    var org_teaminfo = stream.Value<string>("org_team");
                    return new OrgTeams { OrgTeam = org_teaminfo };
                }).ToList();

               

                return new Team { org_teams = org_Teams };
            }).ToListAsync();



            var context = new string[]
            {
                        $"N1QL query - scoped to inventory: {query}; -- {search_orgstream_Name}"
            };

            return (await teams, context);

        }

        public async Task<(IEnumerable<Shape>, string[])> GetOrgShapes(string search_Org_Cluster)
        {
            var query = "SELECT org_shapes" +
                        " FROM `master-data`.`inventory`.`org_teams`" +
            " WHERE org_cluster = $Org_cluster";



            var result = await _MasterInfoContext.Cluster.QueryAsync<dynamic>(query, options => options.Parameter("Org_cluster", search_Org_Cluster));
            var teams = result.Rows.Select(row =>
            {
                var orgShapes = (row.org_shapes as JArray)?.Select(shape =>
                {
                    var orgShape = shape.Value<string>("org_shape");
                    return new Org_Shape { OrgShape = orgShape };
                }).ToList();



                return new Shape { Org_Shapes = orgShapes };
            }).ToListAsync();



            var context = new string[]
            {
        $"N1QL query - scoped to inventory: {query}; -- {search_Org_Cluster}"
            };



            return (await teams, context);



        }


        public async Task<(IEnumerable<ClusterInfo>, string[])> GetOrgCluster(string search_Org_Unit)
        {
            var query = "SELECT org_clusters" +
                        " FROM `master-data`.`inventory`.`org_teams`" +
            " WHERE org_unit  = $org_unit";



            var result = await _MasterInfoContext.Cluster.QueryAsync<dynamic>(query, options => options.Parameter("org_unit", search_Org_Unit));
            var teams = result.Rows.Select(row =>
            {
                var orgClusters = (row.org_clusters as JArray)?.Select(shape =>
                {
                    var orgcluster = shape.Value<string>("org_cluster");
                    return new Org_Cluster { OrgCluster = orgcluster };
                }).ToList();



                return new ClusterInfo { Org_Clusters = orgClusters };
            }).ToListAsync();



            var context = new string[]
            {
        $"N1QL query - scoped to inventory: {query}; -- {search_Org_Unit}"
            };



            return (await teams, context);



        }


    }
}
