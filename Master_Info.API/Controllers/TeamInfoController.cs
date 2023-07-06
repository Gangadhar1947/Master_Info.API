using Microsoft.AspNetCore.Mvc;
using Master_Info.API.Repositories;
using Master_Info.API.Entities;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Search;

namespace Master_Info.API.Controllers
{

    [ApiController]
    [Route("api/v1/[Controller]")]
    public class TeamInfoController : ControllerBase
    {
        private readonly IPersonRepositary _personrepositary;
        private readonly IOrgTeamsRepository _orgTeamsRepository;
        private readonly ILogger<TeamInfoController> _logger;

        public TeamInfoController(IPersonRepositary personrepositary,IOrgTeamsRepository orgTeamsRepository, ILogger<TeamInfoController> logger)
        {
            _personrepositary = personrepositary ?? throw new ArgumentNullException(nameof(personrepositary));
            _orgTeamsRepository = orgTeamsRepository ?? throw new ArgumentNullException(nameof(orgTeamsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
       

        [HttpGet("GetOrgCluster")]
        [ProducesResponseType(typeof(IEnumerable<Shape>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<ClusterInfo>>> GetOrgCluster(string search_org_unit_Name)
        {
            if (string.IsNullOrEmpty(search_org_unit_Name))
            {
                return BadRequest("Missing / invalid arguments.");
            }

            var (teams, context) = await _orgTeamsRepository.GetOrgCluster(search_org_unit_Name);

            return Ok(teams);
        }


        // GET api/Employee/{All info}
        [HttpGet("GetOrgShape")]
        [ProducesResponseType(typeof(IEnumerable<Shape>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Shape>>> GetOrgShapes(string search_orgcluster_Name)
        {
            if (string.IsNullOrEmpty(search_orgcluster_Name))
            {
                return BadRequest("Missing / invalid arguments.");
            }

            var (teams, context) = await _orgTeamsRepository.GetOrgShapes(search_orgcluster_Name);

            return Ok(teams);
        }

        // GET api/Employee/{All info}
        [HttpGet("GetOrgTeams")]
        [ProducesResponseType(typeof(IEnumerable<Team>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Team>>> GetOrgTeams(string search_orgstream_Name)
        {
            if (string.IsNullOrEmpty(search_orgstream_Name))
            {
                return BadRequest("Missing / invalid arguments.");
            }

            var (teams, context) = await _orgTeamsRepository.GetOrgTeams(search_orgstream_Name);

            return Ok(teams);
        }

        // GET api/Employee/{All info}
        [HttpGet("GetPersonInfo")]
        [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons(String search_email_id)
        {
            if (string.IsNullOrEmpty(search_email_id))
            {
                return BadRequest("Missing / invalid arguments.");
            }

            var (persons, context) = await _personrepositary.GetPerson(search_email_id);
            return Ok(new Result(persons, context));
        }






        // POST api/Employee/{Create_all_info}
        [HttpPost("Create_all_Info")]
        [NonAction]
        public async Task<ActionResult<bool>> CreatePerson(string create_id, string create_employee_id,
            string create_first_name, string create_last_name, string create_mobile_no, string create_email_id,
            string create_org_unit, string create_org_team, string create_org_role, string create_location)
        {
            if (create_email_id == null)
            {
                return BadRequest();
            }

            var result = await _personrepositary.CreatePerson(create_id, create_employee_id,
            create_first_name, create_last_name, create_mobile_no, create_email_id,
            create_org_unit, create_org_team, create_org_role, create_location);


            return Ok();


        }




        // PUT api/Employee/{Update_all_info}
        [HttpPut("Update_all_Info")]
        [NonAction]
        public async Task<ActionResult<bool>> UpdatePerson(string update_id, string update_employee_id,
            string update_first_name, string update_last_name, string update_mobile_no, string update_email_id,
            string update_org_unit, string update_org_team, string update_org_role, string update_location)
        {
            if (update_email_id == null)
            {
                return BadRequest();
            }

            var result = await _personrepositary.UpdatePerson(update_id, update_employee_id,
            update_first_name, update_last_name, update_mobile_no, update_email_id,
            update_org_unit, update_org_team, update_org_role, update_location);


            return Ok();


        }

        // PUT api/Employee/{Update_Name}
        [HttpPut("Update_Name")]
        [NonAction]
        public async Task<ActionResult<bool>> UpdatePersonName(string update_first_name, string update_last_name, string update_email_id)
        {
            if (update_email_id == null)
            {
                return BadRequest();
            }

            var result = await _personrepositary.UpdatePersonName(update_first_name, update_last_name, update_email_id);


            return Ok();


        }

        // DELETE api/Employee/{Delete_Name}
        [HttpDelete("Delete_Name")]
        [NonAction]
        public async Task<ActionResult<bool>> DeletePerson(string deleteby_email_id)
        {
            if (deleteby_email_id == null)
            {
                return BadRequest();
            }

            var result = await _personrepositary.DeletePerson(deleteby_email_id);


            return Ok();


        }

    }
}
