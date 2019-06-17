using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Enums;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class WorkhoursViewModel : ComponentBase
    {
        [Parameter] protected DateTime Start
        {
            get
            {
                return Workhours.Start;
            }
            set
            {
                Workhours.Start = CreateNewDatetime(Workhours.Start, value);
            }
        }

        public DateTime End
        {
            get { return Workhours.End; }
            set
            {
                Workhours.End = CreateNewDatetime(Workhours.End, value);
            }
        }

        public DateTime CreateNewDatetime(DateTime oldvalue, DateTime newvalue)
        {
            if (newvalue.Hour != oldvalue.Hour || newvalue.Minute != oldvalue.Minute)
            {
                return new DateTime(oldvalue.Year, oldvalue.Month, oldvalue.Day, newvalue.Hour, newvalue.Minute, newvalue.Second);
            }
            else
            {
                return new DateTime(newvalue.Year, newvalue.Month, newvalue.Day, oldvalue.Hour, oldvalue.Minute, oldvalue.Second);
            }
        }

        [Inject] protected IWorkhours Workhours { get; set; }
        [Inject] protected IWorkhoursService WorkhoursService { get; set; }
        [Inject] protected IStateService StateService { get; set; }

        [Inject] protected IUserService UserService { get; set; }

        private string _selectedStaffMember;

        public string SelectedStaffMember
        {
            get { return _selectedStaffMember; }
            set { _selectedStaffMember = value;
                Workhours.UserId = Int32.Parse(_selectedStaffMember); }
        }

        public List<User> StaffMembers { get; set; } = new List<User>();


        protected override void OnInit()
        {
            if (StateService.CurrentObject is IWorkhours workhours)
            {
                Workhours = workhours;
                SelectedStaffMember = workhours.UserId.ToString();
            }
            if (StateService.OnSetNewCurrentObject != null)
            {
                Workhours = StateService.OnSetNewCurrentObject.Invoke(Workhours) as Workhours;
                StateService.OnSetNewCurrentObject = null;
            }
            base.OnInit();
        }

        protected override async Task OnInitAsync()
        {
            StaffMembers = new List<User>(await UserService.GetStaffByOrganization(StateService.Organization));
        }

        public string Title
        {
            get { return WorkhoursService.GetObjectState(Workhours as Workhours).ToString() + " " + WorkhoursService.GetObjectName(Workhours as Workhours); }
        }

        public void OnClose()
        {
            StateService.CurrentObject = null;
            StateService.CurrentModalType = ModalTypes.None;
            StateService.NotifyStateChanged();
        }

        public async void Save()
        {
            await WorkhoursService.ExecuteAsync(Workhours as Workhours);
            OnClose();
        }

        public async void Delete()
        {
            await WorkhoursService.Delete(Workhours as Workhours);
            OnClose();
        }
    }
}
