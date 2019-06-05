using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentViewModel : ComponentBase
    {
        [Parameter] protected string OrgName { get; set; }
        [Inject] protected IStateService StateService{ get; set; }
        [Inject] protected IOrganizationService OrganizationService { get; set; }
        [Inject] public IEvent Event { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        List<EventOption> ChosenOptions { get; set; } = new List<EventOption>();
        
        protected override async Task OnInitAsync()
        {
            try
            {
                StateService.Organization = await OrganizationService.GetObjectByName(OrgName);
                foreach(Job job in StateService.Organization.Job) {
                    Console.WriteLine(job.Name);
                }
                foreach(User user in StateService.Organization.User) {
                    Console.WriteLine(user.Firstname + " " + user.Lastname);
                }
            } 
            catch
            {
                //
            }
            
            
        }

        public void SetEventJob()
        {
            Event.Job = StateService.Organization.Job.First(job => job.Id == Event.JobId);
        }

        public void SubmitEventOptions()
        {
            //
        }
    }
}