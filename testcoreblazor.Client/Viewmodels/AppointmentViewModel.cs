﻿using BlazorAgenda.Client.Services;
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
        //protected TaskStatus Status { get; set; } = default;
        public TaskStatus Status { get; set; }



        [Inject] protected IOrganizationService OrganizationService { get; set; }

        public List<AppointmentTab> Tabs { get; set; } = new List<AppointmentTab>()
            {
                new AppointmentTab("1. Service") { cssClass= "appointmentTabSelected"},
                new AppointmentTab("2. Date"){ Step=1 },
                new AppointmentTab("3. Further info"){ Step=2 },
                new AppointmentTab("4. Confirmation"){ Step=3 }
            };

        public int Step { get; set; } = 0;

        protected override async Task OnInitAsync()
        {
            StateService.Organization = await OrganizationService.GetObjectByName(OrgName);
        }

        public void PreviousStep()
        {
            Tabs[Step].cssClass = "appointmentTabNormal";
            Step--;
            Tabs[Step].cssClass = "appointmentTabSelected";
            StateHasChanged();
        }

        public void NextStep()
        {
            Tabs[Step].cssClass = "appointmentTabPassed";
            Step++;
            Tabs[Step].cssClass = "appointmentTabSelected";
            StateHasChanged();
        }

        public void ToTab(AppointmentTab selectedTab)
        {
            if (selectedTab.cssClass == "appointmentTabPassed")
            {
                bool isPassed = false;
                foreach (AppointmentTab tab in Tabs)
                {
                    if (isPassed)
                    {
                        tab.cssClass = "appointmentTabNormal";
                    }
                    if (tab == selectedTab)
                    {
                        Step = tab.Step;
                        tab.cssClass = "appointmentTabSelected";
                        isPassed = true;
                    }
                }
                StateHasChanged();
            }
        }

        public async void Commit()
        {
            Event.User = null;
            Event.Job = null;
            Task task = EventService.ExecuteAsync(Event as Event);
            await task.ContinueWith(t => { if (task.IsFaulted) { Console.WriteLine("owh"); } else { Console.WriteLine("ye"); } });
        }
    }

}
