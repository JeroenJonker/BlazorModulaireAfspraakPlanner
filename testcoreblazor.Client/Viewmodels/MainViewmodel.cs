﻿using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class MainViewmodel : ComponentBase
    {
        [Inject]
        protected IStateService StateService { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            StateService.OnChange += StateHasChanged;
        }

        public void OnLoginCompleted(User user)
        {
            StateService.LoginUser = user;
            StateService.ChosenContacts.Add(user);
            StateHasChanged();
        }
    }
}
