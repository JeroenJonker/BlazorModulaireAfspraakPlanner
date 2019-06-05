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
    public class OrganizationService : DefaultObjectService<Organization>, IOrganizationService
    {
        public OrganizationService(HttpClient client) : base(client)
        {
        }
        public async Task<Organization> GetObjectById(int organizationID)
        {
            return await http.GetJsonAsync<Organization>(Resources.OrganizationApi_GetObjectById + organizationID);
        }

        public async Task<Organization> GetObjectByName(string organizationName)
        {
            Organization organization = await http.GetJsonAsync<Organization>(Resources.OrganizationApi_GetObjectByName + organizationName);
            organization.Job = await http.GetJsonAsync<List<Job>>(Resources.JobApi_GetOrganizationJobs + organization.Id);
            return organization;
        }
    }
}
