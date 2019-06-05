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
        [Inject] IStateService StateService { get; set; }
        [Inject] IWorkhoursService WorkhoursService { get; set; }
        [Inject] IAppointment Appointment { get; set; }
        [Inject] IEvent Event { get; set; }

        public List<DateTime> AvailableTimes { get; set; } = new List<DateTime>();

        List<Workhours> Workhours { get; set; } = new List<Workhours>();

        List<EventOption> ChosenOptions { get; set; } = new List<EventOption>();

        [Inject] IOptionService OptionService { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();

        [Inject] protected IEventOptionService EventOptionService { get; set; }
        [Inject] protected IEventService EventService { get; set; }

        protected override async Task OnInitAsync()
        {
            Workhours = await WorkhoursService.GetWorkhours(StateService.LoginUser);
            List<Option> options = await OptionService.GetOptionsAsync(StateService.Organization);
            Options = options.Where(option => option.ElementType == 1 && option.ElementType != default).ToList();
            foreach (Option option in options)
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
            Console.WriteLine("a");
            eventOption.OptionId = eventOption.Option.Id;
            eventOption.Option = default;
            eventOption.EventId = 44;
            ChosenOptions.Add(eventOption as EventOption);
            Console.WriteLine(ChosenOptions.Count());
        }

        public void SubmitEventOptions()
        {
            EventService.ExecuteAsync(Event as Event);
            EventOptionService.PostCollection(ChosenOptions);
        }

        public void OnSelectedTime(DateTime selectedTime)
        {
            Event.Start = selectedTime;
            Event.End = selectedTime.AddMinutes(15);
            Event.UserId = StateService.LoginUser.Id;
        }
    }
}
