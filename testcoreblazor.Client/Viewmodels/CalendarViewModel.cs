﻿using BlazorAgenda.Client.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using BlazorAgenda.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using System.Collections.ObjectModel;

namespace BlazorAgenda.Client.Viewmodels
{
    public class CalendarViewModel : ComponentBase
    {
        [Inject]
        protected EventViewService EventViewService { get; set; }
        [Inject]
        protected IEventService EventService { get; set; }
        [Inject]
        protected IStateService StateService { get; set; }
        [Inject] 
        protected IEvent CurrentObject { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public ObservableCollection<User> Contacts { get; set; }

        public ObservableCollection<User> SelectedContacts { get; set; }

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
            EventViewService.OnSavedChange = CloseEventView;
            SelectedContacts = new ObservableCollection<User>();
            SelectedContacts.Add(StateService.LoginUser);
            UpdateEvents();
            Contacts = new ObservableCollection<User>(await UserService.GetStaffByOrganization(StateService.Organization));
            StateService.OnCollectionChanged = UpdateEvents;
            GoToToday();
        }

        public void UpdateChosenContacts(User user)
        {
            if (SelectedContacts.Contains(user))
            {
                SelectedContacts.Remove(user);
            }
            else
            {
                SelectedContacts.Add(user);
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
            for (int i = 0; i < SelectedContacts.Count; i++)
            {
                List<Event> userEvents = await EventService.GetEvents(SelectedContacts[i]);
                foreach (Event ev in userEvents)
                {
                    Console.WriteLine("UserID: " + ev.UserId);
                    Console.WriteLine("Login user: " + StateService.LoginUser.Id);
                    Console.WriteLine("private : " + (!ev.IsPrivate).ToString());
                    if (!ev.IsPrivate || ev.UserId == StateService.LoginUser.Id)
                    {
                        string color = Colors.Items[i % Colors.Items.Length];
                        events.Add(new CalendarEvent { Event = ev, Color = color });
                    }
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
            if(ViewType == ViewTypes.Day)
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
            EventService.ExecuteAsync(ev as Event);
            StateHasChanged();
        }

        public void OnNewEvent(DateTime start)
        {
            CurrentObject.Start = start;
            CurrentObject.End = start.AddHours(1);
            CurrentObject.UserId = StateService.LoginUser.Id;
            EventViewService.CurrentObject = CurrentObject as Event;
            EventViewService.ChangeVisibility();
        }

        public async Task CloseEventView()
        {
            UpdateEvents();
        }
    }
}
