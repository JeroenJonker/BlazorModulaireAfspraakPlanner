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
        public async Task<List<Organization>> GetOrganizationsAsync()
        {
            return await http.GetJsonAsync<List<Organization>>(Resources.OrganizationApi_GetOrganizations);
        }
    }
}
