using BlazorAgenda.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class OptionDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();
        public IEnumerable<Option> GetOptions(Organization organization)
        {
            // change userid
            IEnumerable<Option> options = db.Option.Where(g => g.Organization == organization).ToList();
            return options;
        }
        public Option GetOption(int id)
        {
            List<Option> options = db.Option.Where(g => g.Id == id).ToList();
            return options.Count > 0 ? options[0] : null;
        }
        public bool TryAddOption(Option newOption)
        {
            try
            {
                db.Option.Add(newOption);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateOption(Option updatedOption)
        {
            try
            {
                db.Entry(updatedOption).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteOption(Option deletedOption)
        {
            try
            {
                db.Option.Remove(deletedOption);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal object GetOptionByParentOption(int parentOptionId)
        {
            return db.Option.Where(g => g.OptionId == parentOptionId).ToList();
        }
    }
}
