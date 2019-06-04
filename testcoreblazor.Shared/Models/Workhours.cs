using BlazorAgenda.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Shared.Models
{
    public partial class Workhours : IWorkhours
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
