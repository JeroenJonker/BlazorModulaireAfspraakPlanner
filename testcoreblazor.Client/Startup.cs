using BlazorAgenda.Client.Services;
using BlazorAgenda.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAgenda.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IStateService, StateService>();
            services.AddSingleton<IOptionService, OptionService>();
            services.AddSingleton<IOrganizationService, OrganizationService>();
            services.AddSingleton<IWorkhoursService, WorkhoursService>();
            services.AddSingleton<IEventOptionService, EventOptionService>();
            services.AddSingleton<IJobService, JobService>();
            services.AddSingleton<IUserJobService, UserJobService>();
            services.AddSingleton<DragDropService>();
            services.AddSingleton<EventViewService>();
            //services.AddSingleton<UserViewService>();
            services.AddTransient<IUser, User>();
            services.AddTransient<IEvent, Event>();
            services.AddTransient<IOption, Option>();
            services.AddTransient<IEventOption, EventOption>();
            services.AddTransient<IWorkhours, Workhours>();
            services.AddTransient<IAppointment, Appointment>();
            services.AddTransient<IJob, Job>();
            services.AddTransient<IUserJob, UserJob>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
