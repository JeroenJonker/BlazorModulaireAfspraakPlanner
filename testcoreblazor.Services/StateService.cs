﻿using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Services
{
    public class StateService : IStateService
    {
        [Inject] public User LoginUser { get; set; }

        public List<User> ChosenContacts { get; set; }
        
        public Pages CurrentPage { get; set; }

        public event Action OnChange;

        public StateService()
        {
            CurrentPage = Pages.Agenda;
            ChosenContacts = new List<User>();
        }

        public void ResetState()
        {
            LoginUser = default(User);
            CurrentPage = default(Pages);
            ChosenContacts.Clear();
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
