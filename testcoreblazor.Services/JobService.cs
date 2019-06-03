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
    }
}