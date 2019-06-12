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

        [Inject] protected IOptionService OptionService { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        [Parameter] Action OnSubmit { get; set; }

        protected override async Task OnInitAsync()
        {
            Options = await OptionService.GetOptionsAsync(StateService.Organization);
        }

        public void AddNewEventOption(IEventOption eventOption)
        {
            eventOption.OptionId = eventOption.Option.Id;
            eventOption.Option = default;
            Event.EventOption.Add(eventOption as EventOption);
        }

        public void SubmitEventOptions()
        {
            OnSubmit?.Invoke();
        }
    }
}
