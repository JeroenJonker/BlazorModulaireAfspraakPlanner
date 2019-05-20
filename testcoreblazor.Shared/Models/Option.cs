using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class Option : IOption
    {
        public Option()
        {
            EventOption = new HashSet<EventOption>();
            InverseOptionNavigation = new HashSet<Option>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int ElementType { get; set; }
        public int? PositionOrder { get; set; }
        public int TimeModifier { get; set; }
        public int? OrganizationId { get; set; }
        public int? OptionId { get; set; }
        public bool IsMandatory { get; set; }

        public Option OptionNavigation { get; set; }
        public Organization Organization { get; set; }
        public ICollection<EventOption> EventOption { get; set; }
        public ICollection<Option> InverseOptionNavigation { get; set; }
    }
}
