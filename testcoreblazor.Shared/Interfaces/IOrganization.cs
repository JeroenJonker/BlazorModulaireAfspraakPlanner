using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Interfaces
{
    public interface IOrganization : IBaseObject
    {
        string Name { get; set; }
        bool IsPrivate { get; set; }

        ICollection<Option> Option { get; set; }
        ICollection<User> User { get; set; }
    }
}
