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
    public class OptionController : Controller, IObjectController<Option>
    {
        OptionDataAccessLayer OptionAccess = new OptionDataAccessLayer();
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Option Object)
        {
            if (OptionAccess.TryAddOption(Object))
            {
                SetSubOptions(Object);
                return CreatedAtAction(nameof(GetObjectById), new { id = Object.Id }, Object);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Option Object)
        {
            if (OptionAccess.TryDeleteOption(Object))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Option Object)
        {
            if (OptionAccess.TryUpdateOption(Object))
            {
                SetSubOptions(Object);
                return Ok(Object);
            }
            return BadRequest();
        }

        private void SetSubOptions(Option Object)
        {
            foreach (Option option in Object.InverseOptionNavigation)
            {
                option.OptionNavigation = null;
            }
        }

        [HttpGet("[action]/{organizationId}")]
        public IActionResult GetOrganizationOptions(int organizationId)
        {
            return Ok(OptionAccess.GetOrganizationOptions(organizationId));
        }

        [HttpGet("[action]/{parentOptionId}")]
        public IActionResult GetOptionByParentOption(int parentOptionId)
        {
            return Ok(OptionAccess.GetOptionByParentOption(parentOptionId));
        }

        private IActionResult GetObjectById(int id)
        {
            if (OptionAccess.GetOption(id) is Option option)
            {
                return Ok(option);
            }
            return NotFound();
        }
    }
}
