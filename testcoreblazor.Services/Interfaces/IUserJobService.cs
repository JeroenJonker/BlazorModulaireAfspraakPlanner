using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IUserJobService : IDefaultObjectService<UserJob>
    {
        Task<List<UserJob>> GetUserJobsByUser(User user);
    }
}