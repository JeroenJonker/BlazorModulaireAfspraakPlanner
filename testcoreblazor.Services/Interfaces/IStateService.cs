using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IStateService
    {
        User LoginUser { get; set; }
        Organization Organization { get; set; }
        //List<User> ChosenContacts { get; set; }
        ModalTypes CurrentModalType { get; set; }
        IBaseObject CurrentObject { get; set; }
        Func<IBaseObject, IBaseObject> OnSetNewCurrentObject { get; set; }
        void ResetState();
        event Action OnChange;
        Pages CurrentPage { get; set; }
        void NotifyStateChanged();
    }
}
