using Microsoft.AspNetCore.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class LoadingViewModel : ComponentBase
    {
        public string Title = "Loading";
        [Parameter] protected bool IsVisibile { get; set; }
    }
}
