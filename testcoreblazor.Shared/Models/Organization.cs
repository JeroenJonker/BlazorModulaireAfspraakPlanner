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
            User = new HashSet<User>();
            Job = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }

        public ICollection<Option> Option { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<Job> Job { get; set; }
    }
}
