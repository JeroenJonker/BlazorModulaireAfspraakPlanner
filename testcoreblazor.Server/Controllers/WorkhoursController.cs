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
    public class WorkhoursController : Controller, IObjectController<Workhours>
    {
        WorkhoursDataAccessLayer WorkhoursAccess = new WorkhoursDataAccessLayer();
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Workhours Object)
        {
            if (WorkhoursAccess.TryAddEvent(Object))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Workhours Object)
        {
            if (WorkhoursAccess.TryDeleteWorkhours(Object))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Workhours Object)
        {
            if (WorkhoursAccess.TryUpdateWorkhours(Object))
            {
                return Ok(Object);
            }
            return BadRequest();
        }

        [HttpGet("[action]/{userid}")]
        public IActionResult GetUserWorkhours(int userid)
        {
            return Ok(WorkhoursAccess.GetUserWorkhours(userid));
        }

        private IActionResult GetObjectById(int id)
        {
            if (WorkhoursAccess.GetWorkhours(id) is Workhours idWorkhours)
            {
                return Ok(idWorkhours);
            }
            return NotFound();
        }
    }
}
