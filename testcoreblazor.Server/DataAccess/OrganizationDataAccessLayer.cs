using BlazorAgenda.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class OrganizationDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();
        public Organization GetOrganization(int id)
        {
            List<Organization> organizations = db.Organization.Where(g => g.Id == id).ToList();
            return organizations.Count > 0 ? organizations[0] : null;
        }
        public bool TryAddOrganization(Organization newOrganization)
        {
            try
            {
                db.Organization.Add(newOrganization);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateOrganization(Organization updatedOrganization)
        {
            try
            {
                db.Entry(updatedOrganization).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteOrganization(Organization deletedOrganization)
        {
            try
            {
                db.Organization.Remove(deletedOrganization);
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
