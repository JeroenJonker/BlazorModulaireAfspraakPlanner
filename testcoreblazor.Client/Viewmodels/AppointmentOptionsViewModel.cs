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
    public class AppointmentOptionsViewModel : ComponentBase
    {
        [Inject] protected IStateService StateService { get; set; }
        [Parameter] [Inject] protected IEvent Event { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        [Parameter] Action OnSubmit { get; set; }
        [Parameter] protected Action ToPreviousTab { get; set; }

        protected override void OnInit()
        {
            Options = StateService.Organization.Option.Where(x => x.TimeModifier == 0 &&
                                     !x.InverseOptionNavigation.Any(y => y.TimeModifier != 0)).ToList();
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
                Event.EventOption.Add(eventOption as EventOption);
            }
        }

        public void SubmitEventOptions()
        {
            OnSubmit?.Invoke();
        }
    }
}
