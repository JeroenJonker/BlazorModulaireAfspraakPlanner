using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IUser : IBaseObject
    {
        string Emailadress { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Password { get; set; }
        bool IsAdmin { get; set; }
        int? OrganizationId { get; set; }
        Organization Organization { get; set; }
        ICollection<Event> Event { get; set; }
        ICollection<UserJob> UserJob { get; set; }
        ICollection<Workhours> Workhours { get; set; }
    }
}
