using BlazorAgenda.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAgenda.Server.DataAccess
{
    public class EventDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public Event GetEvent(int id)
        {
            return db.Event.FirstOrDefault(g => g.Id == id);
        }

        public List<Event> GetUserEvents(int userid)
        {
            try
            {
                return db.Event.Where(g => g.UserId == userid).ToList();
            }
            catch
            {
                return new List<Event>();
            }
            
        }
 
        public bool TryAddEvent(Event newevent)
        {
            try
            {
                db.Event.Add(newevent);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        public bool TryUpdateEvent(Event updatedEvent)
        {
            try
            {
                db.Entry(updatedEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteEvent(Event deletedEvent)
        {
            try
            {
                db.Event.Remove(deletedEvent);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
