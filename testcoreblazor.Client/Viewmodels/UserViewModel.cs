using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UserViewModel : ComponentBase
    {
        [Inject] protected IUser User { get; set; }
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected IStateService StateService { get; set; }

        protected override void OnInit()
        {
            if (StateService.CurrentObject is IUser user)
            {
                User = user;
            }
            base.OnInit();
        }

        public string Title
        {
            get { return UserService.GetObjectState(User as User).ToString() + " " + UserService.GetObjectName(User as User); }
        }

        public void OnClose()
        {
            StateService.CurrentObject = null;
            StateService.CurrentModalType = ModalTypes.None;
            StateService.NotifyStateChanged();
        }

        public async void Save()
        {
            await UserService.ExecuteAsync(User as User);
            OnClose();
        }

        public async void Delete()
        {
            await UserService.Delete(User as User);
            OnClose();
        }
    }
}
