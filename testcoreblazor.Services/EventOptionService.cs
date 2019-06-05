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
    public class EventOptionService : DefaultObjectService<EventOption>, IEventOptionService
    {
        public EventOptionService(HttpClient client) : base(client)
        {

        }
        public async Task PostCollection(List<EventOption> collection)
        {
            await http.PostJsonAsync(Resources.EventOptionApi_PostCollection, collection);
        }
    }
}
