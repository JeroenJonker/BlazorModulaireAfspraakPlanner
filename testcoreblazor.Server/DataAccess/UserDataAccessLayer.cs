using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAgenda.Server.DataAccess
{
    public class UserDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public User GetUser(int id)
        {
            return db.User.Select(g => new User {
                              Id = g.Id, 
                              Emailadress = g.Emailadress, 
                              Firstname = g.Firstname,
                              Lastname = g.Lastname,
                              IsAdmin = g.IsAdmin,
                              OrganizationId = g.OrganizationId
                          })
                          .FirstOrDefault(g => g.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return db.User.FirstOrDefault(g => g.Emailadress == email);
        }

        public User IsValidUser(User user)
        {
            return db.User.FirstOrDefault(g => g.Emailadress == user.Emailadress && g.Password == user.Password);
        }

        public bool TryAddUser(User user)
        {
            try
            {
                db.User.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateUser(User user)
        {
            try
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteUser(User user)
        {
            try
            {
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal IEnumerable<User> GetUsersByOrganization(int organizationId)
        {
            return db.User.Select(g => new User {
                              Id = g.Id, 
                              Emailadress = g.Emailadress, 
                              Firstname = g.Firstname,
                              Lastname = g.Lastname,
                              IsAdmin = g.IsAdmin,
                              OrganizationId = g.OrganizationId
                          }).Where(user => user.OrganizationId == organizationId);
        }
    }
}
