using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IEvent : IBaseEvent
    {
        string Summary { get; set; }
        string Location { get; set; }
        bool IsPrivate { get; set; }
        int JobId { get; set; }
        Job Job { get; set; }
        ICollection<EventOption> EventOption { get; set; }
    }
}
