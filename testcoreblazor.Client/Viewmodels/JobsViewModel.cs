using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Enums;
using System;

namespace BlazorAgenda.Client.Viewmodels
{
    public class JobsViewModel : ComponentBase
    {
        public List<Job> Jobs { get; set; } = new List<Job>();

        [Inject] IJobService JobService { get; set; }
        [Inject] IStateService StateService { get; set; }

        protected override async Task OnInitAsync()
        {
            Jobs = new List<Job>(await JobService.GetJobsAsync(StateService.Organization));
        }

        public void EditJob(Job job)
        {
            StateService.CurrentObject = job;
            StateService.CurrentModalType = ModalTypes.Job;
            StateService.NotifyStateChanged();
        }
        public void AddJob()
        {
            StateService.CurrentModalType = ModalTypes.Job;
            StateService.NotifyStateChanged();
        }
    }
}
