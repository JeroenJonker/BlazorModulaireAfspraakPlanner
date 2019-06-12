using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<Organization> GetOrganizations()
        {
            return db.Organization.ToList();
        }
        public Organization GetOrganization(int id)
        {
            return db.Organization.FirstOrDefault(g => g.Id == id);
        }
        public Organization GetOrganizationByName(string name)
        {
            Organization organization = db.Organization.FirstOrDefault(g => g.Name == name);
            return organization;
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
