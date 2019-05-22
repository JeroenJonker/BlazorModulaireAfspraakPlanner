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
        private string selectedElementType;

        public string SelectedElementType
        {
            get { return selectedElementType; }
            set { selectedElementType = value; Enum.TryParse(selectedElementType, out ElementTypes myStatus); Option.ElementType = (int)myStatus; }
        }

        public string Title
        {
            get { return OptionService.GetObjectState(Option as Option).ToString() + " " + OptionService.GetObjectName(Option as Option); }
        }

        protected override void OnInit()
        {
            AvailableElementTypes = Enum.GetValues(typeof(ElementTypes)).Cast<ElementTypes>().ToList();
            if (StateService.CurrentObject is IOption currentOption)
            {
                Option = currentOption;
            }
            SelectedElementType = nameof(ElementTypes.Text);
            Option.OrganizationId = StateService.Organization.Id;
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
    }
}
