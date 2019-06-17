using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Services
{
    public class AppointmentTab
    {
        public string Name { get; set; }
        public string cssClass { get; set; } = "appointmentTabNormal";
        public int Step { get; set; } = 0;

        public AppointmentTab(string name)
        {
            Name = name;
        }
    }
}
