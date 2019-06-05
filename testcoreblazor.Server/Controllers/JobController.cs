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
    public class JobController : Controller, IObjectController<Job>
    {
        JobDataAccessLayer JobAccess = new JobDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Job Object)
        {
            if (JobAccess.TryAddJob(Object))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Job Object)
        {
            if (JobAccess.TryUpdateJob(Object))
            {
                return Ok(Object);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Job Object)
        {
            if (JobAccess.TryDeleteJob(Object))
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpGet("[action]/{organizationId}")]
        public IActionResult GetOrganizationJobs(int organizationId)
        {
            return Ok(JobAccess.GetOrganizationJobs(organizationId));
        }

        private IActionResult GetObjectById(int id)
        {
            if (JobAccess.GetJob(id) is Job job)
            {
                return Ok(job);
            }
            return NotFound();
        }
    }
}
