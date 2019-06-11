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
    public class OptionsViewModel : ComponentBase
    {
        public List<Option> Options { get; set; }

        [Inject] IStateService StateService { get; set; }

        [Inject] IOptionService OptionService { get; set; }

        [Inject] DragDropService DragDropService { get; set; }

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

        protected void OnDrop(Option dropOption)
        {
            List<Option> newOrder = new List<Option>();
            foreach (Option option in Options)
            {
                if (option == dropOption)
                {
                    Option dragOption = DragDropService.Data as Option;
                    dragOption.PositionOrder = newOrder.Count;
                    newOrder.Add(DragDropService.Data as Option);
                }
                if (option != DragDropService.Data as Option)
                {
                    option.PositionOrder = newOrder.Count;
                    newOrder.Add(option);
                }
            }
        }

        protected void OnDragStart(Option option)
        {
            DragDropService.StartDrag(option, "");
        }

        // public async Task CloseOptionView()
        // {
        //     Options = await OptionService.GetOptionsAsync(StateService.Organization);
        //     StateHasChanged();
        // }
    }
}
