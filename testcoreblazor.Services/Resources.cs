using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services
{
    public static class Resources
    {
        public const string ControllerApi = "api/";

        public const string ObjectApi_Edit = "/Edit";

        public const string ObjectApi_Add = "/Add";

        public const string ObjectApi_Delete = "/Delete";

        public const string EventApi_GetUserEvents = "api/Event/GetUserEvents/";

        public const string UserApi_IsUserInUse = "api/User/IsUserInUse";

        public const string UserApi_IsValidUser = "api/User/IsValidUser";

        public const string OrganizationApi_GetOrganizations = "api/Organization/GetOrganizations/";
        
        public const string OrganizationApi_GetObjectById = "api/Organization/GetObjectById/";

        public const string OrganizationApi_GetObjectByName = "api/Organization/GetObjectByName/";

        public const string OptionsApi_GetOrganizationOptions = "api/Option/GetOrganizationOptions/";

        public const string UserApi_GetStaffByOrganization = "api/User/GetStaffByOrganization/";

        public const string WorkhoursApi_GetUserWorkhours = "api/Workhours/GetUserWorkhours/";

        public const string JobApi_GetOrganizationJobs = "api/Job/GetOrganizationJobs/";
        
        public const string JobApi_GetJobsByUser = "api/Job/GetJobsByUser/";

        public const string UserJobApi_GetUserJobsByUser = "api/UserJob/GetUserJobsByUser/";

        public const string EventOptionApi_PostCollection = "api/EventOption/PostCollection/";
    }
}
