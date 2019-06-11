using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationController : Controller, IObjectController<Organization>
    {
        OrganizationDataAccessLayer OrganizationAccess = new OrganizationDataAccessLayer();
        UserDataAccessLayer UserAccess = new UserDataAccessLayer();
        JobDataAccessLayer JobAccess = new JobDataAccessLayer();
        UserJobDataAccessLayer UserJobAccess = new UserJobDataAccessLayer();
        OptionDataAccessLayer OptionAccess = new OptionDataAccessLayer();


        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Organization Object)
        {
            if (OrganizationAccess.TryAddOrganization(Object))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Organization Object)
        {
            if (OrganizationAccess.TryDeleteOrganization(Object))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Organization Object)
        {
            if (OrganizationAccess.TryUpdateOrganization(Object))
            {
                return Ok(Object);
            }
            return BadRequest();
        }

        [HttpGet("[action]/{organizationID}")]
        public IActionResult GetObjectById(int organizationID)
        {
            if (OrganizationAccess.GetOrganization(organizationID) is Organization organization)
            {
                return Ok(organization);
            }
            return NotFound();
        }

        [HttpGet("[action]/{organizationName}")]
        public IActionResult GetObjectByName(string organizationName)
        {
            if (OrganizationAccess.GetOrganizationByName(organizationName) is Organization organization)
            {
                organization.Job = JobAccess.GetOrganizationJobs(organization.Id).ToList();
                organization.User = UserAccess.GetUsersByOrganization(organization.Id).ToList();
                foreach(Job job in organization.Job) {
                    job.UserJob = UserJobAccess.GetUserJobs(job.Id).ToList();
                }
                organization.Option = OptionAccess.GetOrganizationOptions(organization.Id).ToList();
                return Ok(organization);
            }
            return NotFound();
        }
    }
}
