using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.DataAccess
{
    public class JobDataAccessLayer
    {
        AgendaDBContext db = new AgendaDBContext();

        public Job GetJob(int id)
        {
            List<Job> jobs = db.Job.Where(g => g.Id == id).ToList();
            return jobs.Count > 0 ? jobs[0] : null;
        }

        public List<Job> GetAllJobs()
        {
            return db.Job.ToList();
        }

        public bool TryAddJob(Job newJob)
        {
            try
            {
                db.Job.Add(newJob);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryUpdateJob(Job job)
        {
            try
            {
                db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteJob(Job job)
        {
            try
            {
                db.Job.Remove(job);
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