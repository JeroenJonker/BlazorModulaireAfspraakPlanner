using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlazorAgenda.Services
{
    public class OrganizationService : DefaultObjectService<Organization>, IOrganizationService
    {
        public OrganizationService(HttpClient client) : base(client)
        {
        }
    }
}
