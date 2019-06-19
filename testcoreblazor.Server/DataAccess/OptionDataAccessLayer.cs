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
            foreach (Option option in options)
            {
                GetSubOptions(option);
            }
            options = options.OrderBy(x => x.PositionOrder);
            return options.ToList();
        }
        public Option GetOption(int id)
        {
            Option option = db.Option.FirstOrDefault(g => g.Id == id);
            option = GetSubOptions(option);
            return option;
        }

        private Option GetSubOptions(Option option)
        {
            option.InverseOptionNavigation = db.Option.Select(g => new Option {
                                                          Id = g.Id,
                                                          Text = g.Text,
                                                          Description = g.Description,
                                                          ElementType = g.ElementType,
                                                          PositionOrder = g.PositionOrder,
                                                          TimeModifier = g.TimeModifier,
                                                          OrganizationId = g.OrganizationId,
                                                          OptionId = g.OptionId,
                                                          IsMandatory = g.IsMandatory
                                                      }).Where(g => g.OptionId == option.Id)
                                                      .ToList();
            return option;
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
                foreach (Option option in updatedOption.InverseOptionNavigation)
                {
                    if (option.Id == default)
                    {
                        TryAddOption(option);
                    }
                    else
                    {
                        TryUpdateOption(option);
                    }
                }
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
                db.Option.RemoveRange(deletedOption.InverseOptionNavigation);
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
