using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces.BaseObjects
{
    public interface IBaseEvent : IBaseObject
    {
        DateTime Start { get; set; }
        DateTime End { get; set; }
        int UserId { get; set; }

        User User { get; set; }
    }
}
