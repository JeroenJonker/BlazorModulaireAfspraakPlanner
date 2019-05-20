﻿using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class EventOptionController : Controller, IObjectController<EventOption>
    {
        EventOptionDataAccessLayer EventOptionAccess = new EventOptionDataAccessLayer();
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] EventOption Object)
        {
            if (EventOptionAccess.TryAddEventOption(Object))
            {
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public IActionResult Delete([FromBody] EventOption Object)
        {
            if (EventOptionAccess.TryDeleteEventOption(Object))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public IActionResult Edit([FromBody] EventOption Object)
        {
            if (EventOptionAccess.TryUpdateEventOption(Object))
            {
                return Ok(Object);
            }
            return BadRequest();
        }

        private IActionResult GetObjectById(int id)
        {
            if (EventOptionAccess.GetEventOption(id) is EventOption eventOption)
            {
                return Ok(eventOption);
            }
            return NotFound();
        }
    }
}
