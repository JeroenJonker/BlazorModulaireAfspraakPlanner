using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IUserJob : IBaseObject
    {
        int UserId { get; set; }
        int JobId { get; set; }
        User User { get; set; }
        Job Job { get; set; }
    }
}
