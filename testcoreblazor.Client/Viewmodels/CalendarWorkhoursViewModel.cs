using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarWorkhoursViewModel : ComponentBase
    {
        [Inject]
        protected IWorkhoursService WorkhoursService { get; set; }
        [Inject]
        protected IStateService StateService { get; set; }
        [Inject]
        protected IWorkhours Workhours { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Parameter]
        protected DateTime Start { get; set; }
        public ObservableCollection<User> Contacts { get; set; } = new ObservableCollection<User>();

        public ObservableCollection<User> SelectedUsers { get; set; } = new ObservableCollection<User>();

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                GoToSelectedDate();
            }
        }
        public DateTime StartOfWeekDate { get; set; }

        public string CurrentMonthAndYear { get; set; }

        public enum ViewTypes
        {
            Day = 1, Week = 7, WorkWeek = 5
        };

        private ViewTypes viewType = ViewTypes.Week;
        public ViewTypes ViewType
        {
            get { return viewType; }
            set
            {
                viewType = value;
                GoToSelectedDate();
            }
        }

        protected override async Task OnInitAsync()
        {
            Contacts = SelectedUsers = new ObservableCollection<User>(await UserService.GetStaffByOrganization(StateService.Organization));
            UpdateEvents();
            StateService.OnCollectionChanged = UpdateEvents;
            GoToToday();
        }

        public void UpdateChosenContacts(User user)
        {
            if (SelectedUsers.Contains(user))
            {
                SelectedUsers.Remove(user);
            }
            else
            {
                SelectedUsers.Add(user);
            }
            UpdateEvents();
        }

        public async void UpdateEvents()
        {
            List<CalendarEvent> events = await GetCalendarEvents();
            DragDropHelper.Items = events.OrderBy(x => x.Event.Start).ToList();
            StateHasChanged();
        }

        public async Task<List<CalendarEvent>> GetCalendarEvents()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            for (int i = 0; i < SelectedUsers.Count; i++)
            {
                List<Workhours> userEvents = await WorkhoursService.GetWorkhours(SelectedUsers[i]);
                foreach (Workhours ev in userEvents)
                {
                    string color = Colors.Items[i % Colors.Items.Length];
                    events.Add(new CalendarEvent { Event = ev, Color = color });
                }
            }
            return events;
        }

        public void GoToPrevious()
        {
            if (ViewType == ViewTypes.Day)
                SelectedDate = SelectedDate.AddDays(-1);
            else
                SelectedDate = SelectedDate.AddDays(-7);
        }

        public void GoToToday()
        {
            SelectedDate = DateTime.Today;
        }

        public void GoToNext()
        {
            if (ViewType == ViewTypes.Day)
                SelectedDate = SelectedDate.AddDays(1);
            else
                SelectedDate = SelectedDate.AddDays(7);
        }

        public void GoToSelectedDate()
        {
            int delta = DayOfWeek.Monday - SelectedDate.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            StartOfWeekDate = SelectedDate.AddDays(delta);
            CurrentMonthAndYear = GetCurrentMonthAndYear();
            StateHasChanged();
        }

        public string GetCurrentMonthAndYear()
        {
            if (ViewType == ViewTypes.Day)
            {
                return SelectedDate.ToString("dd MMMM yyyy");
            }

            string startMonth = StartOfWeekDate.ToString("MMMM");
            string startYear = StartOfWeekDate.ToString("yyyy");
            DateTime endOfWeekDate = StartOfWeekDate.AddDays(6);
            string endMonth = endOfWeekDate.ToString("MMMM");
            string endYear = endOfWeekDate.ToString("yyyy");
            string monthAndYear;

            if (endYear == startYear)
            {
                if (endMonth == startMonth)
                    monthAndYear = startMonth + " " + startYear;
                else
                    monthAndYear = startMonth + " - " + endMonth + " " + startYear;
            }
            else
            {
                monthAndYear = startMonth + " " + startYear + " - " + endMonth + " " + endYear;
            }

            return monthAndYear;
        }

        public void OnMoveEvent(IBaseEvent ev)
        {
            WorkhoursService.ExecuteAsync(ev as Workhours);
            StateHasChanged();
        }

        public void OnNewEvent(DateTime start)
        {
            Start = start;
            StateService.CurrentModalType = BlazorAgenda.Shared.Enums.ModalTypes.Workhours;
            StateService.OnSetNewCurrentObject = SetNewCurrentObject;
            Console.WriteLine("check");
            StateService.NotifyStateChanged();
        }

        public IBaseObject SetNewCurrentObject(IBaseObject newObject)
        {
            Workhours newObjects = newObject as Workhours;
            newObjects.Start = Start;
            return newObjects;
        }
    }
}
