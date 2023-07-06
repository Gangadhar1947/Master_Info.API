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
namespace Master_Info.API.Repositories
{
   
        public class PersonRepositary : IPersonRepositary
        {
            private readonly ITeamInfoContext _MasterInfoContext;

            public PersonRepositary(ITeamInfoContext MasterInfoContext)
            {
                _MasterInfoContext = MasterInfoContext ?? throw new ArgumentNullException(nameof(MasterInfoContext));
                // _bucket = EmployeeContext.MasterDataBucket;
            }


            public async Task<(IEnumerable<Person>, string[])> GetPerson(string search_email_id)
            {
                var query = "SELECT id,employee_id,first_name,last_name,mobile_no,email_id,org_unit,org_team,org_role,location" +
                            " FROM `master-data`.`inventory`.`persons`" +
                            " WHERE email_id = $email_id";

                //if (search_email_id != null)
                //{

                //  query += " WHERE email_id = $email_id";

                // }


                var personResult = await _MasterInfoContext.Cluster.QueryAsync<Person>(query, options => options.Parameter("email_id", search_email_id));

                var q = await personResult.Rows.ToListAsync();

                var context = new string[]
                {
                $"N1QL query - scoped to inventory: {query}; -- {search_email_id}"
                };

                return (q, context);

            }

            public async Task<bool> CreatePerson(string create_id, string create_employee_id,
            string create_first_name, string create_last_name, string create_mobile_no, string create_email_id,
            string create_org_unit, string create_org_team, string create_org_role, string create_location)
            {
                var create_query = "INSERT INTO `master-data`.`inventory`.`persons`(KEY,VALUE) " +
                                   $"VALUES ($document_id, {{ " +
                                   " `id`=$id," +
                                   " `employee_id`=$employee_id," +
                                   " `first_name`=$first_name," +
                                   " `last_name`=$last_name, " +
                                   " `mobile_no`=$mobile_no," +
                                   " `email_id`=$email_id," +
                                   " `org_unit`=$org_unit," +
                                   " `org_team`=$org_team, " +
                                   " `org_role`=$org_role," +
                                   " `location`=$location" +
                                   " }" +
                                   " }" +
                                   ") ";

                var create_queryResult = await _MasterInfoContext.Cluster.QueryAsync<Person>(create_query, options =>
                options.Parameter("document_id", "emp_60800589")
                    .Parameter("id", create_id)
                    .Parameter("employee_id", create_employee_id)
                    .Parameter("first_name", create_first_name)
                    .Parameter("last_name", create_last_name)
                    .Parameter("mobile_no", create_mobile_no)
                    .Parameter("email_id", create_email_id)
                    .Parameter("org_unit", create_org_unit)
                    .Parameter("org_team", create_org_team)
                    .Parameter("org_role", create_org_role)
                    .Parameter("location", create_location)
                    );


                return (true);
            }

            public async Task<bool> UpdatePerson(string update_id, string update_employee_id,
                string update_first_name, string update_last_name, string update_mobile_no, string update_email_id,
                string update_org_unit, string update_org_team, string update_org_role, string update_location)
            {
                var update_query = "UPDATE `master-data`.`inventory`.`persons`" +
                                   " SET" +
                                   " id=$id," +
                                   " employee_id=$employee_id," +
                                   " first_name=$first_name," +
                                   " last_name=$last_name, " +
                                   " mobile_no=$mobile_no," +
                                   " email_id=$email_id," +
                                   " org_unit=$org_unit," +
                                   " org_team=$org_team, " +
                                   " org_role=$org_role," +
                                   " location=$location" +
                                   " WHERE email_id = $email_id";

                var update_queryResult = await _MasterInfoContext.Cluster.QueryAsync<Person>(update_query, options =>
                options.Parameter("id", update_id)
                    .Parameter("employee_id", update_employee_id)
                    .Parameter("first_name", update_first_name)
                    .Parameter("last_name", update_last_name)
                    .Parameter("mobile_no", update_mobile_no)
                    .Parameter("email_id", update_email_id)
                    .Parameter("org_unit", update_org_unit)
                    .Parameter("org_team", update_org_team)
                    .Parameter("org_role", update_org_role)
                    .Parameter("location", update_location)
                    );


                return (true);

            }

            public async Task<bool> UpdatePersonName(string update_first_name, string update_last_name, string update_email_id)
            {
                var update_query = "UPDATE `master-data`.`inventory`.`persons`" +
                                   " SET first_name=$first_name,last_name=$last_name WHERE $email_id = $email_id";

                var update_queryResult = await _MasterInfoContext.Cluster.QueryAsync<Person>
                    (update_query, options => options.Parameter("first_name", update_first_name)
                                                     .Parameter("last_name", update_last_name)
                                                     .Parameter("email_id", update_email_id)
                    );


                return (true);

            }

            public async Task<bool> DeletePerson(string deleteby_email_id)
            {
                var delete_query = "DELETE FROM `master-data`.`inventory`.`persons` WHERE email_id = $email_id";

                var delete_queryResult = await _MasterInfoContext.Cluster.QueryAsync<Person>(delete_query, options => options.Parameter("email_id", deleteby_email_id));

                return (true);
            }


        }
    
}
