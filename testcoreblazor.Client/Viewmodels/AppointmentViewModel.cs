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
    public class AppointmentViewModel : ComponentBase
    {
        [Parameter] protected string OrgName { get; set; }
        [Inject] protected IStateService StateService { get; set; }
        [Inject] protected IEventService EventService { get; set; }
        [Parameter][Inject] protected IEvent Event { get; set; }

        [Inject] protected IOrganizationService OrganizationService { get; set; }

        public List<KeyValuePair<string, string>> Tabs { get; set; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("1. Dienst", "appointmentTabSelected"),
            new KeyValuePair<string, string>("2. Datum", "appointmentTabNormal"),
            new KeyValuePair<string, string>("3. Algemene gegevens", "appointmentTabNormal"),
            new KeyValuePair<string, string>("4. Bevestiging", "appointmentTabNormal")
        };

        public int Step { get; set; } = 0;

        protected override async Task OnInitAsync()
        {
            StateService.Organization = await OrganizationService.GetObjectByName(OrgName);
        }

        public void NextStep()
        {
            Tabs[Step] = new KeyValuePair<string,string>(Tabs[Step].Key,"appointmentTabNormal");
            Step++;
            Tabs[Step] = new KeyValuePair<string, string>(Tabs[Step].Key, "appointmentTabSelected");
            StateHasChanged();
        }

        public void Commit()
        {
            //                eventOption.Option = null; stten
            Event.User = null;
            Event.Job = null;
            EventService.ExecuteAsync(Event as Event);
        }

        public void Check()
        {
            Console.WriteLine(Event.JobId);
            Console.WriteLine(Event.UserId);
            Console.WriteLine(Event.Start.ToString());
            Console.WriteLine(Event.End.ToString());

        }
    }

}
