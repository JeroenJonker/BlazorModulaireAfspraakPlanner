using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Components;
using BlazorAgenda.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class OptionViewModel : ComponentBase
    {
        [Inject] protected IOption Option {get;set;}
        [Inject] protected IOptionService OptionService { get; set; }
        [Inject] protected IStateService StateService { get; set; }
        public List<ElementTypes> AvailableElementTypes { get; set; }

        public List<Option> DropdownItemOptions { get; set; } = new List<Option>();
        private string selectedElementType;

        public string SelectedElementType
        {
            get { return selectedElementType; }
            set
            {
                selectedElementType = value;
                Enum.TryParse(selectedElementType, out ElementTypes myStatus);
                Option.ElementType = (int)myStatus;
                StateHasChanged();
            }
        }

        public string Title
        {
            get { return OptionService.GetObjectState(Option as Option).ToString() + " " + OptionService.GetObjectName(Option as Option); }
        }

        protected override void OnInit()
        {
            AvailableElementTypes = Enum.GetValues(typeof(ElementTypes)).Cast<ElementTypes>().ToList();
            SelectedElementType = Enum.GetName(typeof(ElementTypes), ElementTypes.Text);
            if (StateService.CurrentObject is IOption currentOption)
            {
                Option = currentOption;
                SelectedElementType = Enum.GetName(typeof(ElementTypes), (ElementTypes)Option.ElementType);
                DropdownItemOptions = Option.InverseOptionNavigation.ToList();
            }
            Option.OrganizationId = StateService.Organization.Id;
        }

        protected override async Task OnInitAsync()
        {
            if (Option.PositionOrder == null)
            {
                Option.PositionOrder = 1;
                List<Option> options = new List<Option>();
                options = await OptionService.GetOptionsAsync(StateService.Organization);
                if (options.Aggregate((x, y) => x.PositionOrder > y.PositionOrder ? x : y).PositionOrder is int lastorder)
                {
                    Option.PositionOrder += lastorder;
                }
            }
        }

        public void OnClose()
        {
            StateService.CurrentObject = null;
            StateService.CurrentModalType = ModalTypes.None;
            StateService.NotifyStateChanged();
        }

        public async void Save()
        {
            await OptionService.ExecuteAsync(Option as Option);
            OnClose();
        }

        public async void Delete()
        {
            await OptionService.Delete(Option as Option);
            OnClose();
        }

        public void AddDropdownItem()
        {
            Option newDropdownItem = new Option() { ElementType = (int)ElementTypes.Text, OrganizationId = StateService.Organization.Id };
            Option.InverseOptionNavigation.Add(newDropdownItem);
            DropdownItemOptions.Add(newDropdownItem);
        }

        public void RemoveDropdownItem(Option dropdownitem)
        {
            Option.InverseOptionNavigation.Remove(dropdownitem);
            DropdownItemOptions.Remove(dropdownitem);
        }
    }
}
