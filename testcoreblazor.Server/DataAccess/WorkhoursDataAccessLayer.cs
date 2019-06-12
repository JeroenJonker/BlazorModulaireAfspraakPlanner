using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class WorkhoursDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public List<Workhours> GetUserWorkhours(int userid)
        {
            return db.Workhours.Where(g => g.UserId == userid).ToList();
        }

        public bool TryAddEvent(Workhours newWorkhours)
        {
            try
            {
                db.Workhours.Add(newWorkhours);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateWorkhours(Workhours updatedWorkhours)
        {
            try
            {
                db.Entry(updatedWorkhours).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteWorkhours(Workhours deletedWorkhours)
        {
            try
            {
                db.Workhours.Remove(deletedWorkhours);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Workhours GetWorkhours(int id)
        {
            return db.Workhours.FirstOrDefault(g => g.Id == id);
        }
    }
}
