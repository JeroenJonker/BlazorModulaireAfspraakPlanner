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
        protected List<User> SelectedJobUsers{ get; set; } = new List<User>();
        
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

        public void SetEventJob(UIChangeEventArgs e)
        {
            SelectedJobUsers.Clear();
            Event.UserId = -1;
            
            Event.JobId = Int32.Parse(e.Value.ToString());
            foreach (Job job in StateService.Organization.Job) {
                Console.WriteLine(job.Id);
            }
            Event.Job = StateService.Organization.Job.First(job => job.Id == Event.JobId);

            foreach (User user in StateService.Organization.User.Join(Event.Job.UserJob, u => u.Id, uj => uj.UserId, (u, uj) => u))
            {
                SelectedJobUsers.Add(user);
            }
        }

        public void SubmitEventOptions()
        {
            //
        }
    }
}