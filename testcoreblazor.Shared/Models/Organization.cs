using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class Organization : IOrganization
    {
        public Organization()
        {
            Option = new HashSet<Option>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }

        public ICollection<Option> Option { get; set; }
    }
}
