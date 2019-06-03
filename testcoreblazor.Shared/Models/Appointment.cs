using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Models
{
    public class Appointment : IAppointment
    {
        public int TimeModifier { get; set; }

        public List<EventOption> EventOptions { get; set; }
        public Organization Organization { get; set; }

        public User ChosenStaffMember { get; set; }

        public List<EventOption> ChosenEventOptions { get; set; }
    }
}
