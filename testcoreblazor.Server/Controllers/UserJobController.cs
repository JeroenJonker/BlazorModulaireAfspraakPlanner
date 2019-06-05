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
    public class UserJobController : Controller, IObjectController<UserJob>
    {
        UserJobDataAccessLayer UserJobAccess = new UserJobDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] UserJob Object)
        {
            if (UserJobAccess.TryAddUserJob(Object))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] UserJob Object)
        {
            if (UserJobAccess.TryUpdateUserJob(Object))
            {
                return Ok(Object);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] UserJob Object)
        {
            if (UserJobAccess.TryDeleteUserJob(Object))
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpGet("[action]/{organizationId}")]
        public IActionResult GetUserJobs(int jobId)
        {
            return Ok(UserJobAccess.GetUserJobs(jobId));
        }

        private IActionResult GetObjectById(int id)
        {
            if (UserJobAccess.GetUserJob(id) is UserJob userUserJob)
            {
                return Ok(userUserJob);
            }
            return NotFound();
        }
    }
}