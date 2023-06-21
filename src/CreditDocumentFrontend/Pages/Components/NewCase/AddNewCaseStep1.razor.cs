using Microsoft.AspNetCore.Components;
using SharedModels;

namespace CreditDocumentFrontend.Pages.Components.NewCase;

public partial class AddNewCaseStep1
{
    [CascadingParameter] public CreditDocument CreditDocument { get; set; }
    
    [Parameter] public EventCallback CloseModal { get; set; }
    [Parameter] public EventCallback GoToStep2 { get; set; }
    
    private void HandleValidSubmit()
    {
        // Logger.LogInformation("HandleValidSubmit called");

        // Process the valid form
        GoToStep2.InvokeAsync();
    }

    private void CloseModalNow()
    {
        CloseModal.InvokeAsync();
    }
}