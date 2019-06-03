using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IEvent : IBaseEvent
    {
        string Summary { get; set; }
        string Location { get; set; }
        bool IsPrivate { get; set; }

    }
}
