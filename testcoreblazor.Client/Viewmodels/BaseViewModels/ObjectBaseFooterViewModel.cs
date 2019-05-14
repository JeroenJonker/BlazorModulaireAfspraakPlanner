using BlazorAgenda.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorAgenda.Client.Viewmodels.BaseViewModels
{
    public class ObjectBaseFooterViewModel : ComponentBase
    {
        [Parameter] protected Action OnSave { get; set; }
        [Parameter] protected Action OnDelete { get; set; }
        [Parameter] protected ObjectState ObjectState { get; set; }
    }
}
