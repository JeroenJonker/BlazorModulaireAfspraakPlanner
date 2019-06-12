using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public class UserService : DefaultObjectService<User>, IUserService
    {
        public UserService(HttpClient client) : base(client)
        {
        }

        public async Task<User> CheckUser(User user)
        {
            try
            {
                return await http.PostJsonAsync<User>(Resources.UserApi_IsValidUser, user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsUserInUse(User user)
        {
            return await http.PostJsonAsync<bool>(Resources.UserApi_IsUserInUse, user);
        }

        public async Task<List<User>> GetStaffByOrganization(Organization organization)
        {
            return await http.GetJsonAsync<List<User>>(Resources.UserApi_GetStaffByOrganization + organization.Id);
        }
    }
}
