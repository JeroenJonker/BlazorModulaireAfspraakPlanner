using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class JobService : DefaultObjectService<Job>, IJobService
    {
        public JobService(HttpClient client) : base(client)
        {
        }
        public async Task<List<Job>> GetJobsAsync(Organization organization)
        {
            return await http.GetJsonAsync<List<Job>>(Resources.JobApi_GetOrganizationJobs + organization.Id);
        }
        public async Task<List<Job>> GetJobsByUserAsync(User user)
        {
            return await http.GetJsonAsync<List<Job>>(Resources.JobApi_GetJobsByUser + user.Id);
        }
    }
}