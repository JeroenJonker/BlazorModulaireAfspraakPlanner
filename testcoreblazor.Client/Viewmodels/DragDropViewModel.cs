using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorAgenda.Client.Viewmodels
{
    public class DragDropViewModel : ComponentBase
    {
        [Parameter]
        protected DateTime Start { get; set; }

        [Parameter]
        protected Action<IBaseEvent> MoveEvent { get; set; }

        [Parameter]
        protected Action<DateTime> NewEvent { get; set; }

        [Inject]
        protected IStateService StateService { get; set; }

        public string HighlightDropTargetStyle { get; set; }

        public void OnItemDragStart(UIDragEventArgs e, CalendarEvent calendarEvent)
        {
            if ((calendarEvent.Event is Event && calendarEvent.Event.UserId == StateService.LoginUser.Id) || calendarEvent.Event is Workhours)
            {
                DragDropHelper.Item = calendarEvent;
            }
        }

        public void OnContainerDragEnter(UIDragEventArgs e)
        {
            if (DragDropHelper.Item != null)
            {
                HighlightDropTargetStyle = "background-color: #0069d9 !important;";
            }
        }

        public void OnContainerDragLeave(UIDragEventArgs e)
        {
            HighlightDropTargetStyle = string.Empty;
        }

        public void OnContainerDrop(UIDragEventArgs e, DateTime _start)
        {
            HighlightDropTargetStyle = "";
            UpdateEvent(_start);
            DragDropHelper.Item = null;
        }

        private void UpdateEvent(DateTime _start)
        {
            IBaseEvent item = DragDropHelper.Item.Event;
            TimeSpan duration = item.End - item.Start;
            item.Start = _start;
            item.End = _start.Add(duration);
            MoveEvent?.Invoke(item);
        }

        public void OnContainerClick()
        {
            if(DragDropHelper.Items.FindAll(x => x.Event.Start == Start).Count == 0)
            {
                NewEvent?.Invoke(Start);
            }
        }
    }
}
