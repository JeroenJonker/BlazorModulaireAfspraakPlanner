using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IEventOption : IBaseObject
    {
        int EventId { get; set; }
        int OptionId { get; set; }
        string Value { get; set; }

        Event Event { get; set; }
        Option Option { get; set; }
    }
}
