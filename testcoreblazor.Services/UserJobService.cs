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
    public class UserJobService : DefaultObjectService<UserJob>, IUserJobService
    {
        public UserJobService(HttpClient client) : base(client)
        {
        }
        public async Task<List<UserJob>> GetUserJobsByUser(User user)
        {
            return await http.GetJsonAsync<List<UserJob>>(Resources.UserJobApi_GetUserJobsByUser + user.Id);
        }
    }
}