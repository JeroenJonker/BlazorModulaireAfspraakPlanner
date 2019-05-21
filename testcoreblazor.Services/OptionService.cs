﻿using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class OptionService : DefaultObjectService<Option>, IOptionService
    {
        public OptionService(HttpClient client) : base(client)
        {
        }

        public async Task<List<Option>> GetOptionsAsync(User user)
        {
            return await http.GetJsonAsync<List<Option>>(Resources.EventApi_GetUserEvents + user.Id);
        }
    }
}
