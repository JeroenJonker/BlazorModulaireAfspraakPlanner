using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class NavigationBarViewModel : ComponentBase
    {
        [Inject] protected IStateService StateService { get; set; }

        public void EditUser()
        {
            StateService.CurrentObject = StateService.LoginUser;
            StateService.CurrentModalType = ModalTypes.User;
            StateService.NotifyStateChanged();
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
            //StateService.CurrentModalType = ModalTypes.Option;
            StateService.NotifyStateChanged();
        }
    }
}
