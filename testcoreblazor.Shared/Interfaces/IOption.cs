using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IOption : IBaseObject
    {
        string Text { get; set; }
        string Description { get; set; }
        int ElementType { get; set; }
        int? PositionOrder { get; set; }
        int TimeModifier { get; set; }
        int? OrganizationId { get; set; }
        int? OptionId { get; set; }

        Option OptionNavigation { get; set; }
        Organization Organization { get; set; }
        ICollection<EventOption> EventOption { get; set; }
        ICollection<Option> InverseOptionNavigation { get; set; }
    }
}
