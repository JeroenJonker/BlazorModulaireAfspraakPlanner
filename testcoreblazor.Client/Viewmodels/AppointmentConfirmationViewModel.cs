using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentConfirmationViewModel : ComponentBase
    {
        [Parameter] protected Action OnSubmit { get; set; }
        [Parameter] [Inject] protected IEvent Event { get; set; }
        [Parameter] protected Action ToPreviousTab { get; set; }
        [Inject] protected IEventService EventService { get; set; }
        [Parameter] public TaskStatus Status { get; set; }

        public async void Commit()
        {
            //foreach (EventOption eventOption in Event.EventOption)
            //{
            //    eventOption.Option = null;
            //}
            //Event.User = null;
            //Event.Job = null;
            Task task = EventService.ExecuteAsync(Event as Event);
            await task.ContinueWith(t => { if (task.Status == TaskStatus.Faulted) { Console.WriteLine("owh"); } else { Console.WriteLine("ye"); } });
            Status = task.Status;
            StateHasChanged();
            //OnSubmit?.Invoke();
        }

    }
}
