using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class UserJobDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public UserJob GetUserJob(int id)
        {
            return db.UserJob.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<UserJob> GetUserJobs(int jobId)
        {
            return db.UserJob.Where(g => g.Job.Id == jobId);;
        }

        public bool TryAddUserJob(UserJob newUserJob)
        {
            try
            {
                db.UserJob.Add(newUserJob);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateUserJob(UserJob userJob)
        {
            try
            {
                db.Entry(userJob).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteUserJob(UserJob userJob)
        {
            try
            {
                db.UserJob.Remove(userJob);
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