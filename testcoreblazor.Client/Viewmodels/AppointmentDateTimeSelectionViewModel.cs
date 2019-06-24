using BlazorAgenda.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using BlazorAgenda.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentDateTimeSelectionViewModel : ComponentBase
    {
        [Inject] protected IWorkhoursService WorkhoursService { get; set; }
        [Inject] protected IEventService EventService { get; set; }
        [Parameter][Inject] protected IEvent Event { get; set; }
        public List<DateTime> AvailableTimes { get; set; } = new List<DateTime>();
        public List<Workhours> Workhours { get; set; } = new List<Workhours>();
        public List<Event> Events { get; set; } = new List<Event>();
        public int EventDuration { get; set; } = 0;
        [Parameter] protected Action OnSubmit { get; set; }
        [Parameter] protected Action ToPreviousTab { get; set; }


        protected override async Task OnInitAsync()
        {
            Workhours = await WorkhoursService.GetWorkhours(Event.User);
            Events = await EventService.GetEvents(Event.User);
            EventDuration += Event.Job.TimeModifier;
            foreach (EventOption eventOption in Event.EventOption)
            {
                if (!(eventOption.Option.ElementType == (int)ElementTypes.Check && eventOption.Value == "True"))
                {
                    EventDuration += eventOption.Option.TimeModifier;
                }
            }
            if (Event.Start != default)
            {
                OnSelectedDate(Event.Start);
            }
        }

        public void OnSelectedDate(DateTime date)
        {
            AvailableTimes = new List<DateTime>();
            foreach (Workhours workhours in Workhours)
            {
                if (workhours.Start.Year == date.Year && workhours.Start.Month == date.Month && workhours.Start.Day == date.Day)
                {
                    SetAvailableTimes(workhours);
                }
            }
            StateHasChanged();
        }

        public void SetAvailableTimes(Workhours workhour)
        {
            DateTime start = workhour.Start;
            while (start.AddMinutes(EventDuration) <= workhour.End)
            {
                if (!IsTimeInConflictWithEvents(start, start.AddMinutes(EventDuration)) && start >= DateTime.Now)
                {
                    AvailableTimes.Add(start);
                }
                start = start.AddMinutes(15);
            }
        }

        public bool IsTimeInConflictWithEvents(DateTime start, DateTime end)
            => Events.Any(time => (start >= time.Start && start < time.End) || 
            (end > time.Start && end <= time.End) || 
            (start <= time.Start && end >= time.End));

        public void OnSelectedTime(DateTime selectedTime)
        {
            Event.Start = selectedTime;
            Event.End = selectedTime.AddMinutes(EventDuration);
            Submit();
        }

        public void Submit()
        {
            OnSubmit?.Invoke();
        }
    }
}
