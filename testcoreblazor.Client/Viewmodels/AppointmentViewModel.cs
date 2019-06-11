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
        protected List<User> SelectedJobUsers{ get; set; } = new List<User>();
        
        protected override async Task OnInitAsync()
        {
            try
            {
                StateService.Organization = await OrganizationService.GetObjectByName(OrgName);
                
                Options = StateService.Organization.Option.Where(x => x.TimeModifier != 0 || 
                                                                 (x.InverseOptionNavigation.Count != 0 && 
                                                                 x.InverseOptionNavigation.FirstOrDefault(y => y.TimeModifier != 0) != null)).ToList();

                foreach(Job job in StateService.Organization.Job) {
                    Console.WriteLine(job.Name);
                }
                foreach(User user in StateService.Organization.User) {
                    Console.WriteLine(user.Firstname + " " + user.Lastname);
                }
                foreach(Option option in Options) {
                    Console.WriteLine(option.Text);
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

        public void AddNewEventOption(IEventOption eventOption)
        {
            eventOption.OptionId = eventOption.Option.Id;
            eventOption.Option = default;
            Event.EventOption.Add(eventOption);
        }
    }
}