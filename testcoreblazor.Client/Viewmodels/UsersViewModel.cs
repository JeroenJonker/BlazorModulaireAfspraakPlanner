﻿using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Enums;
using System;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UsersViewModel : ComponentBase
    {
        public List<User> Users { get; set; } = new List<User>();

        [Inject] IUserService UserService { get; set; }
        [Inject] IStateService StateService { get; set; }

        protected override void OnInit()
        {
            StateService.OnCollectionChanged = GetUsers;
            GetUsers();
        }

        public async void GetUsers()
        {
            Users = new List<User>(await UserService.GetStaffByOrganization(StateService.Organization));
            StateHasChanged();

        }

        public void EditUser(User user)
        {
            StateService.CurrentObject = user;
            StateService.CurrentModalType = ModalTypes.User;
            StateService.NotifyStateChanged();
        }
        public void AddUser()
        {
            StateService.CurrentModalType = ModalTypes.User;
            StateService.NotifyStateChanged();
        }

        //public async Task CloseUserView()
        //{
        //    Users = await UserService.GetContacts();
        //    StateHasChanged();
        //}
    }
}
