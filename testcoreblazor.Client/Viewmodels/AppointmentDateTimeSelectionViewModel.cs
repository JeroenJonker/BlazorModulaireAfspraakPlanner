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
        [Inject] protected IStateService StateService { get; set; }
        [Inject] protected IWorkhoursService WorkhoursService { get; set; }
        [Inject] protected IAppointment Appointment { get; set; }
        [Inject] protected IEvent Event { get; set; }

        public List<DateTime> AvailableTimes { get; set; } = new List<DateTime>();

        public List<Workhours> Workhours { get; set; } = new List<Workhours>();

        [Inject] protected IOptionService OptionService { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();

        [Inject] protected IEventOptionService EventOptionService { get; set; }
        [Inject] protected IEventService EventService { get; set; }

        protected override async Task OnInitAsync()
        {
            Workhours = await WorkhoursService.GetWorkhours(StateService.LoginUser);
            Options = await OptionService.GetOptionsAsync(StateService.Organization);
            foreach (Option option in Options)
            {
                Console.WriteLine(option.ElementType);
            }
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

        public void AddNewEventOption(IEventOption eventOption)
        {
            eventOption.OptionId = eventOption.Option.Id;
            eventOption.Option = default;
            Event.EventOption.Add(eventOption as EventOption);
        }

        public void SubmitEventOptions()
        {
            EventService.ExecuteAsync(Event as Event);
        }

        public void OnSelectedTime(DateTime selectedTime)
        {
            Event.Start = selectedTime;
            Event.End = selectedTime.AddMinutes(15);
            Event.UserId = StateService.LoginUser.Id;
        }
    }
}
