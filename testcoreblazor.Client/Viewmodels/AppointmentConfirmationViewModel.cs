using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentConfirmationViewModel : ComponentBase
    {
        [Parameter] protected Action OnSubmit { get; set; }
        [Parameter] [Inject] protected IEvent Event { get; set; }

        public void Commit()
        {
            foreach (EventOption eventOption in Event.EventOption)
            {
                eventOption.Option = null;
            }
            OnSubmit?.Invoke();
        }

    }
}
