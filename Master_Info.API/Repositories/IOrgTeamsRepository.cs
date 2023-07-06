using Master_Info.API.Entities;

namespace Master_Info.API.Repositories
{
    public interface IOrgTeamsRepository
    {

        Task<(IEnumerable<Team>, string[])> GetOrgTeams(string orgstreamName);

        Task<(IEnumerable<Shape>, string[])> GetOrgShapes(string search_orgcluster_Name);

        Task<(IEnumerable<ClusterInfo>, string[])> GetOrgCluster(string search_orgunit_Name);

    }
}