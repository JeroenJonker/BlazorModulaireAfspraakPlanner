using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UserViewModel : ComponentBase
    {
        [Inject] protected IUser User { get; set; }
        [Inject] protected IUserJobService UserJobService { get; set; }
        [Inject] protected IUserService UserService { get; set; }
        [Inject] protected IJobService JobService { get; set; }
        [Inject] protected IStateService StateService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        protected List<Job> Jobs { get; set; } = new List<Job>();

        protected ElementRef multiSelect;

        protected override async Task OnInitAsync()
        {
            if (StateService.CurrentObject is IUser user)
            {
                User = user;
                User.UserJob = await UserJobService.GetUserJobsByUser(User as User);
            }
            Jobs = await JobService.GetJobsAsync(StateService.Organization);
            User.OrganizationId = StateService.Organization.Id;
            base.OnInit();
        }

        public string Title
        {
            get { return UserService.GetObjectState(User as User).ToString() + " " + UserService.GetObjectName(User as User); }
        }

        public void OnClose()
        {
            StateService.CurrentObject = null;
            StateService.CurrentModalType = ModalTypes.None;
            StateService.NotifyStateChanged();
        }

        public async void Save()
        {
            await UserService.ExecuteAsync(User as User);
            OnClose();
        }

        public async void Delete()
        {
            await UserService.Delete(User as User);
            OnClose();
        }

        public async void OnMultiSelectChange(UIChangeEventArgs e)
        {
            List<string> selectedList = await JSRuntime.InvokeAsync<List<string>>(
            "JsFunctions.getSelectedList", multiSelect);

            User.UserJob = new List<UserJob>();
            
            if (selectedList.Count != 0) {
                foreach (string option in selectedList)
                {
                    User.UserJob.Add(new UserJob() { JobId = Int32.Parse(option), UserId = User.Id });
                }
            }
        }
    }
}
