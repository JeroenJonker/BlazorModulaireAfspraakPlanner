using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IJobService : IDefaultObjectService<Job>
    {
        Task<List<Job>> GetJobsAsync(Organization organization);
        Task<List<Job>> GetJobsByUserAsync(User user);
    }
}