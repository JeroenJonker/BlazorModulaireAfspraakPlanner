using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class OptionsViewModel : ComponentBase
    {
        public List<Option> Options { get; set; }

        [Inject] IStateService StateService { get; set; }

        protected override void OnInit()
        {
            Options = new List<Option>();
            Console.WriteLine(StateService.LoginUser.Organization.Name);
            //Organization organization = StateService.LoginUser.Organization;
            base.OnInit();
        }
    }
}
