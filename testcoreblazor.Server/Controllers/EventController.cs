using BlazorAgenda.Server.DataAccess;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller, IObjectController<Event>
    {
        EventDataAccessLayer EventAccess = new EventDataAccessLayer();
        OptionDataAccessLayer OptionAccess = new OptionDataAccessLayer();
        EventOptionDataAccessLayer EventOptionAccess = new EventOptionDataAccessLayer();

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (EventAccess.TryAddEvent(newEvent))
            {
                foreach (EventOption eventOption in newEvent.EventOption)
                {
                    eventOption.Event = null;
                }
                return CreatedAtAction(nameof(GetObjectById), new { id = newEvent.Id }, newEvent);
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody] Event updateEvent)
        {
            if (EventAccess.TryUpdateEvent(updateEvent))
            {
                return Ok(updateEvent);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody] Event deleteEvent)
        {
            if (EventAccess.TryDeleteEvent(deleteEvent))
            {
                return Ok();
            }
            return BadRequest();
        }

        private IActionResult GetObjectById(int id)
        {
            if (EventAccess.GetEvent(id) is Event idEvent)
            {
                return Ok(idEvent);
            }
            return NotFound();
        }

        [HttpGet("[action]/{userid}")]
        public IActionResult GetUserEvents(int userid)
        {
            List<Event> UserEvents = EventAccess.GetUserEvents(userid);
            foreach (Event userEvent in UserEvents)
            {
                userEvent.EventOption = EventOptionAccess.GetEventOptionsByEventId(userEvent.Id).ToList();
                foreach (EventOption eventOption in userEvent.EventOption)
                {
                    eventOption.Option = OptionAccess.GetOption(eventOption.OptionId);
                }
            }
            return Ok(UserEvents);
        }
    }
}
