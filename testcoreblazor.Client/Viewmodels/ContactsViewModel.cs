using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlazorAgenda.Client.Viewmodels
{
    public class ContactsViewModel : ComponentBase
    {
        //[Inject]
        //public IUserService UserService { get; set; }
        //[Inject]
        //public IStateService StateService { get; set; }

        [Parameter]
        protected Action<User> OnSelectedContact { get; set; }

        [Parameter]
        protected ObservableCollection<User> Contacts { get; set; } = new ObservableCollection<User>();
        [Parameter]
        protected ObservableCollection<User> ChosenContacts { get; set; } = new ObservableCollection<User>();

        protected override void OnInit()
        {
            base.OnInit();
        }

        //public void SelectContact(int userId, bool selected)
        //{
        //    User user = ChosenContacts.Find(x => x.Id == userId);
        //    if (selected && user == null)
        //    {
        //        user = Contacts.Find(x => x.Id == userId);
        //        ChosenContacts.Add(user);
        //    }
        //    else if (!selected && user != null)
        //    {
        //        ChosenContacts.Remove(user);
        //    }
        //    OnUpdate?.Invoke(user);
        //}
    }
}
