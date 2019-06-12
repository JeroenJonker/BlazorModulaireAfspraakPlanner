using BlazorAgenda.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using BlazorAgenda.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Services.Interfaces;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentDateTimeSelectionViewModel : ComponentBase
    {
        [Inject] protected IWorkhoursService WorkhoursService { get; set; }
        [Parameter][Inject] protected IEvent Event { get; set; }
        public List<DateTime> AvailableTimes { get; set; } = new List<DateTime>();
        public List<Workhours> Workhours { get; set; } = new List<Workhours>();
        [Parameter] protected Action OnSubmit { get; set; }

        protected override async Task OnInitAsync()
        {
            Workhours = await WorkhoursService.GetWorkhours(Event.User);
        }

        public void OnSelectedDate(DateTime date)
        {
            AvailableTimes = new List<DateTime>();
            foreach (Workhours workhours in Workhours)
            {
                if (workhours.Start.Year == date.Year && workhours.Start.Month == date.Month && workhours.Start.Day == date.Day)
                {
                    AvailableTimes.Add(workhours.Start);
                }
            }
            StateHasChanged();
        }

        public void OnSelectedTime(DateTime selectedTime)
        {
            Event.Start = selectedTime;
            Event.End = selectedTime.AddMinutes(15);
            Submit();
        }

        public void Submit()
        {
            OnSubmit?.Invoke();
        }
    }
}
