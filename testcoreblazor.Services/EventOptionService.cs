using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlazorAgenda.Services
{
    public class EventOptionService : DefaultObjectService<EventOption>, IEventOptionService
    {
        public EventOptionService(HttpClient client) : base(client)
        {
        }
    }
}
