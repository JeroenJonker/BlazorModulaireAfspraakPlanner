using BlazorAgenda.Client.Services;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAgenda.Shared.Enums;

namespace BlazorAgenda.Client.Viewmodels
{
    public class OptionsViewModel : ComponentBase
    {
        public List<Option> Options { get; set; }

        [Inject] IStateService StateService { get; set; }

        [Inject] IOptionService OptionService { get; set; }

        protected override async Task OnInitAsync()
        {
            Options = new List<Option>();
            Options = await OptionService.GetOptionsAsync(StateService.Organization);
        }

        public void EditOption(Option option)
        {
            StateService.CurrentObject = option;
            StateService.CurrentModalType = ModalTypes.Option;
            StateService.NotifyStateChanged();
        }
        public void AddOption()
        {
            StateService.CurrentModalType = ModalTypes.Option;
            StateService.NotifyStateChanged();
        }

        // public async Task CloseOptionView()
        // {
        //     Options = await OptionService.GetOptionsAsync(StateService.Organization);
        //     StateHasChanged();
        // }
    }
}
