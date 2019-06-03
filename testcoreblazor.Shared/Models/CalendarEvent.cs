using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Interfaces.BaseObjects;

namespace BlazorAgenda.Shared.Models
{
    public partial class CalendarEvent : ICalendarEvent
    {
        public int Id { get; set; }
        public IBaseEvent Event { get; set; }
        public string Color { get; set; }
    }
}
