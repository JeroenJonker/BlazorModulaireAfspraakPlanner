using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IAppointment
    {
        int TimeModifier { get; set; }

        List<EventOption> EventOptions { get; set; }
        Organization Organization { get; set; }

        User ChosenStaffMember { get; set; }

        List<EventOption> ChosenEventOptions { get; set; }

        Event Event { get; set; }

    }
}
