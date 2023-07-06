using System.Text.Json.Serialization;

namespace Master_Info.API.Entities
{
    public class Team
    {

        [JsonPropertyName("org_teamslist")]
        public List<OrgTeams>? org_teams { get; set; }

       

    }
    public class OrgTeams
    {
        [JsonPropertyName("org_team")]
        public string? OrgTeam { get; set; }
    }

   
   
}
