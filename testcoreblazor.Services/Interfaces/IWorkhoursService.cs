using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IWorkhoursService : IDefaultObjectService<Workhours>
    {
        Task<List<Workhours>> GetWorkhours(User user);
    }
}
