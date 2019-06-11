using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IEventOptionService : IDefaultObjectService<EventOption>
    {
        //kan weg
        Task PostCollection(List<EventOption> collection);
    }
}
