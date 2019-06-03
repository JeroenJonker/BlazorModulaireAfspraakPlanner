using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Services
{
    public class DateCell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public DateTime Date { get; set; }
        public string StyleClass { get; set; }
    }
}
