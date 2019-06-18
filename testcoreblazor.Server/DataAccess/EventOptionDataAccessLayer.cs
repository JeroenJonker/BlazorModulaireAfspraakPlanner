using BlazorAgenda.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class EventOptionDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public EventOption GetEventOption(int id)
        {
            return db.EventOption.FirstOrDefault(g => g.Id == id);
        }
        public bool TryAddEventOption(EventOption newEventOption)
        {
            try
            {
                db.EventOption.Add(newEventOption);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateEventOption(EventOption updatedEventOption)
        {
            try
            {
                db.Entry(updatedEventOption).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteEventOption(EventOption deletedEventOption)
        {
            try
            {
                db.EventOption.Remove(deletedEventOption);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<EventOption> GetEventOptionsByEventId (int eventId)
        {
            try
            {
                return db.EventOption.Where(eventOption => eventOption.EventId == eventId);
            }
            catch
            {
                return Enumerable.Empty<EventOption>();
            }
        }
    }
}
