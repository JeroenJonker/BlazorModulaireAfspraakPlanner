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
    public class AppointmentJobViewModel : ComponentBase
    {
        [Inject] protected IStateService StateService{ get; set; }
        [Parameter] [Inject] public IEvent Event { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        protected List<User> SelectedJobUsers{ get; set; } = new List<User>();
        [Parameter] protected Action OnSubmit { get; set; }
        
        protected override void OnInit()
        {
            Options = StateService.Organization.Option.Where(x => x.TimeModifier != 0 || 
                                                                 (x.InverseOptionNavigation.Count != 0 && 
                                                                 x.InverseOptionNavigation.Any(y => y.TimeModifier != 0))).ToList();
            if (Event.JobId != default)
            {
                foreach (User user in StateService.Organization.User.Join(Event.Job.UserJob, u => u.Id, uj => uj.UserId, (u, uj) => u))
                {
                    SelectedJobUsers.Add(user);
                }
            }
        }

        public void SetEventJob(UIChangeEventArgs e)
        {
            SelectedJobUsers.Clear();
            Event.UserId = -1;
            
            Event.JobId = int.Parse(e.Value.ToString());
            Event.Job = StateService.Organization.Job.First(job => job.Id == Event.JobId);
            Event.Summary = Event.Job.Name;

            foreach (User user in StateService.Organization.User.Join(Event.Job.UserJob, u => u.Id, uj => uj.UserId, (u, uj) => u))
            {
                SelectedJobUsers.Add(user);
            }
        }

        public void SetEventUser(UIChangeEventArgs e)
        {
            Event.UserId = int.Parse(e.Value.ToString());
            Event.User = SelectedJobUsers.FirstOrDefault(user => user.Id == Event.UserId);
        }

        public void SubmitEventOptions()
        {
            OnSubmit?.Invoke();
        }

        public void AddNewEventOptions(List<EventOption> eventOptions)
        {
            foreach (IEventOption eventOption in eventOptions) {
                eventOption.OptionId = eventOption.Option.Id;
                Event.EventOption.Add(eventOption as EventOption);
            }
        }

        public void SetMultiEventOptions(List<EventOption> eventOptions, Option option)
        {
            foreach (EventOption eventOption in Event.EventOption.Where(eo => eo.OptionId == option.Id)) {
                Event.EventOption.Remove(eventOption);
            }
            foreach (EventOption eventOption in eventOptions) {
                eventOption.OptionId = eventOption.Option.Id;
                Event.EventOption.Add(eventOption);
            }
        }
    }
}