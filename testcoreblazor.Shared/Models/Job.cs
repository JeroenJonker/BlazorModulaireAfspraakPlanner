using BlazorAgenda.Shared.Interfaces;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class Job : IJob
    {
        public Job()
        {
            UserJob = new HashSet<UserJob>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }
        public ICollection<UserJob> UserJob { get; set; }
        public ICollection<Event> Event { get; set; }
    }
}