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
        public List<Option> GetOrganizationOptions(int organizationId)
        {
            IEnumerable<Option> options = db.Option.Where(g => g.Organization.Id == organizationId && g.OptionId == null);
            options = GetSubOptions(options).OrderBy(x => x.PositionOrder);
            return options.ToList();
        }
        public Option GetOption(int id)
        {
            List<Option> options = db.Option.Where(g => g.Id == id).ToList();
            options = GetSubOptions(options).ToList();
            return options.Count > 0 ? options[0] : null;
        }

        private IEnumerable<Option> GetSubOptions(IEnumerable<Option> options)
        {
            foreach (Option option in options)
            {
                option.InverseOptionNavigation = db.Option.Where(g => g.OptionId == option.Id).ToList();
                foreach (Option subOption in option.InverseOptionNavigation)
                {
                    subOption.OptionNavigation = null;
                }
            }
            return options;
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
