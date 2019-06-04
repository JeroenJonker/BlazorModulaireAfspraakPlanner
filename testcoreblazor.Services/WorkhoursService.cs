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
    public class WorkhoursService : DefaultObjectService<Workhours>, IWorkhoursService
    {
        public WorkhoursService(HttpClient client) : base(client)
        {

        }
        public async Task<List<Workhours>> GetWorkhours(User user)
        {
            return await http.GetJsonAsync<List<Workhours>>(Resources.WorkhoursApi_GetUserWorkhours + user.Id);
        }
    }
}
