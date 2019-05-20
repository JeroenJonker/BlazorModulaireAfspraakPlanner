using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class EventOption : IEventOption
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; }

        public Event Event { get; set; }
        public Option Option { get; set; }
    }
}
