using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoginViewmodel : ComponentBase
    {
        [Parameter] Action<User, Organization> OnLogin { get; set; }
        [Inject] protected IUser User { get; set; }
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected IOrganizationService OrganizationService { get; set; }

        public string Style { get; set; }
        public bool IsLoggingIn { get; set; } = false;

        public async void LoginAsync()
        {
            IsLoggingIn = true;
            Regex r = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");
            if (User.Emailadress != null && User.Password != null && r.IsMatch(User.Emailadress))
            {
                if (await UserService.CheckUser(User as User) is User checkedUser)
                {
                    Style = "";
                    Organization organization = await OrganizationService.GetObjectById(checkedUser.OrganizationId.Value);
                    OnLogin?.Invoke(checkedUser, organization);
                }
                else
                {
                    User.Password = null;
                    Style = "border-color: red;";
                    StateHasChanged();
                }
            }
            else
            {
                Style = "border-color: red;";
            }
            IsLoggingIn = false;
        }
    }
}
