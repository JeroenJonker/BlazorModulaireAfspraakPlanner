using BlazorAgenda.Shared.Interfaces;
using System.Collections.Generic;
using System;

namespace BlazorAgenda.Shared.Models
{
    public partial class Event : IEvent
    {
        public Event()
        {
            EventOption = new HashSet<EventOption>();
        }
        public int Id { get; set; }
        public string Summary { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public int Userid { get; set; }
        public bool IsPrivate { get; set; }

        public User User { get; set; }
        public ICollection<EventOption> EventOption { get; set; }
    }
}
