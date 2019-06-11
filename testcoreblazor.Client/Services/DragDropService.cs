using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Services
{
    public class DragDropService
    {
        public object Data { get; set; }
        public string Zone { get; set; }

        public void StartDrag(object data, string zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}
