using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class NavigationBarViewModel : ComponentBase
    {
        [Inject] protected IStateService StateService { get; set; }
        [Inject] protected UserViewService UserView { get; set; }

        public void EditUser()
        {
            UserView.CurrentObject = StateService.LoginUser;
            UserView.ChangeVisibility();
        }

        public void Logout()
        {
            StateService.ResetState();
            StateService.NotifyStateChanged();
        }

        public void ViewUsers()
        {
            StateService.CurrentPage = Pages.Users;
            StateService.NotifyStateChanged();
        }

        public void ViewAgenda()
        {
            StateService.CurrentPage = Pages.Agenda;
            StateService.NotifyStateChanged();
        }

        public void ViewOptions()
        {
            StateService.CurrentPage = Pages.Options;
            StateService.NotifyStateChanged();
        }
    }
}
