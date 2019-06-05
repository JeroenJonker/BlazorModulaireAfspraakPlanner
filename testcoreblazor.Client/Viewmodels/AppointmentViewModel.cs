using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AppointmentViewModel : ComponentBase
    {
        [Parameter] protected string Text { get; set; } = "fantastic";
        [Inject] protected IOrganizationService OrganizationService { get; set; }
        
        protected override async Task OnInitAsync()
        {
            try
            {
                Organization organization = await OrganizationService.GetObjectByName(Text);
                foreach(Job job in organization.Job) {
                    Console.WriteLine(job.Name);
                }
                foreach(User user in organization.User) {
                    Console.WriteLine(user.Firstname + " " + user.Lastname);
                }
            } 
            catch
            {
                //
            }
            
            
        }
    }
}