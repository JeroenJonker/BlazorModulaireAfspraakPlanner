using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IJob : IBaseObject
    {
        string Name { get; set; }
        string Description { get; set; }
        ICollection<UserJob> UserJob { get; set; }
    }
}