using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class JobViewModel : ComponentBase
    {
        [Inject] protected IJob Job { get; set; }
        [Inject] protected IJobService JobService { get; set; }
        [Inject] protected IStateService StateService { get; set; }

        protected override void OnInit()
        {
            if (StateService.CurrentObject is IJob job)
            {
                Job = job;
            }
            base.OnInit();
        }

        public string Title
        {
            get { return JobService.GetObjectState(Job as Job).ToString() + " " + JobService.GetObjectName(Job as Job); }
        }

        public void OnClose()
        {
            StateService.CurrentObject = null;
            StateService.CurrentModalType = ModalTypes.None;
            StateService.NotifyStateChanged();
        }

        public async void Save()
        {
            await JobService.ExecuteAsync(Job as Job);
            OnClose();
        }

        public async void Delete()
        {
            await JobService.Delete(Job as Job);
            OnClose();
        }
    }
}
