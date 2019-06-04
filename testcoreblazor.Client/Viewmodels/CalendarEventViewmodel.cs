using System;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Client.Services;
using BlazorAgenda.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarEventViewModel : ComponentBase
    {
        [Inject]
        protected EventViewService UserView { get; set; }

        [Inject]
        protected IStateService StateService { get; set; }

        [Parameter]
        protected ICalendarEvent CalendarEvent { get; set; }

        [Parameter]
        protected int Rowspan { get; set; }

        [Parameter]
        protected int NumEvents { get; set; }

        [Parameter]
        protected Action<UIDragEventArgs, CalendarEvent> DragStart { get; set; }

        public bool ShowModalEvent { get; set; } = false;

        public void ChangeShowModalEvent()
        {
            if (CalendarEvent.Event is Event && CalendarEvent.Event.UserId == StateService.LoginUser.Id)
            {
                UserView.CurrentObject = CalendarEvent.Event as Event;
                UserView.ChangeVisibility();
            }
            else if (CalendarEvent.Event is Workhours)
            {
                StateService.CurrentObject = CalendarEvent.Event;
                StateService.CurrentModalType = BlazorAgenda.Shared.Enums.ModalTypes.Workhours;
                StateService.NotifyStateChanged();
            }
        }
    }
}
