using System.Text.Json.Serialization;

namespace Master_Info.API.Entities
{
   
        public class Person
        {
            //[JsonPropertyName("document_id")]
            //public string Document_Id { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("employee_id")]
            public string Employee_Id { get; set; }

            [JsonPropertyName("first_name")]
            public string First_Name { get; set; }

            [JsonPropertyName("last_name")]
            public string Last_Name { get; set; }

            [JsonPropertyName("mobile_no")]
            public string Mobile_No { get; set; }

            [JsonPropertyName("email_id")]
            public string Email_Id { get; set; }

            [JsonPropertyName("org_unit")]
            public string Org_Unit { get; set; }

            [JsonPropertyName("org_team")]
            public string Org_Team { get; set; }

            [JsonPropertyName("org_role")]
            public string Org_role { get; set; }

            [JsonPropertyName("location")]
            public string Location { get; set; }

            [JsonPropertyName("type")]
            public string Type => "BSH.OPS.Tools.MasterData.Person";

            [JsonPropertyName("documentType")]
            public string DocumentType => "BSH.OPS.Tools.MasterData.Person";
        }
   
}
