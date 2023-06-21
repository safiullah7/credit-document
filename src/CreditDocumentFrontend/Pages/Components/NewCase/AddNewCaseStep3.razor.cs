using Microsoft.AspNetCore.Components;
using SharedModels;

namespace CreditDocumentFrontend.Pages.Components.NewCase;

public partial class AddNewCaseStep3
{
    [CascadingParameter] public CreditDocument CreditDocument { get; set; }
    
    [Parameter] public EventCallback CloseModal { get; set; }
    [Parameter] public EventCallback GoToStep3 { get; set; }
}