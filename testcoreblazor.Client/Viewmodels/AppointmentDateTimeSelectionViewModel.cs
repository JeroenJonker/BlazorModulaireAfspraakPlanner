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

        public List<DateTime> AvailableTimes { get; set; } = new List<DateTime>(0);

        List<Workhours> Workhours { get; set; } = new List<Workhours>();

        [Inject] IOptionService OptionService { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();

        protected override async Task OnInitAsync()
        {
            Workhours = await WorkhoursService.GetWorkhours(StateService.LoginUser);
            List<Option> ptions = await OptionService.GetOptionsAsync(StateService.Organization);
            Options = ptions.Where(option => option.ElementType == 1 && option.ElementType != null || option.ElementType != default).ToList();
            foreach (Option option in ptions)
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
    }
}
