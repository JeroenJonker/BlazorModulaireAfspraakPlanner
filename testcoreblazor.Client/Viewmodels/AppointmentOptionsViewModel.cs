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

        protected override void OnInit()
        {
            Options = StateService.Organization.Option.Where(x => x.TimeModifier == 0 &&
                                     !x.InverseOptionNavigation.Any(y => y.TimeModifier != 0)).ToList();
        }

        public void AddNewEventOption(IEventOption eventOption)
        {
            eventOption.OptionId = eventOption.Option.Id;
            Event.EventOption.Add(eventOption as EventOption);
        }

        public void SubmitEventOptions()
        {
            OnSubmit?.Invoke();
        }
    }
}
