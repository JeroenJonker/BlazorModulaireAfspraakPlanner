using BlazorAgenda.Shared.Interfaces;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class User : IUser
    {
        public User()
        {
            Event = new HashSet<Event>();
            Workhours = new HashSet<Workhours>();
        }

        public int Id { get; set; }
        public string Emailadress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<Workhours> Workhours { get; set; }
    }
}
