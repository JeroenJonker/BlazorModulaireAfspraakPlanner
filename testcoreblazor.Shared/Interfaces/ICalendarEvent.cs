using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface ICalendarEvent : IBaseObject
    {
        IBaseEvent Event { get; set; }
        string Color { get; set; }
    }
}
