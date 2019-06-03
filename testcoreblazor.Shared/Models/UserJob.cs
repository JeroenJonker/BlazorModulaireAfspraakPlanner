using BlazorAgenda.Shared.Interfaces;
using System.Collections.Generic;

namespace BlazorAgenda.Shared.Models
{
    public partial class UserJob : IUserJob
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }

        public User User { get; set; }
        public Job Job { get; set; }
    }
}
