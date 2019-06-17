using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Services
{
    public class StateService : IStateService
    {
        [Inject] public User LoginUser { get; set; }
        public Organization Organization { get; set; }
        
        public Pages CurrentPage { get; set; }

        public ModalTypes CurrentModalType { get; set; }
        public IBaseObject CurrentObject { get; set; }

        public event Action OnChange;

        public Action OnCollectionChanged { get; set; }

        public Func<IBaseObject,IBaseObject> OnSetNewCurrentObject { get; set; }

        public StateService()
        {
            CurrentPage = Pages.Agenda;
        }

        public void ResetState()
        {
            LoginUser = default(User);
            CurrentPage = default(Pages);
            CurrentObject = null;
            Organization = null;
        }

        public void NotifyStateChanged()
        {
            OnCollectionChanged?.Invoke();
            OnChange?.Invoke();
        }
    }
}
