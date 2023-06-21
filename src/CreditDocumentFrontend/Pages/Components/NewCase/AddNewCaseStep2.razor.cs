using Blazorise;
using Microsoft.AspNetCore.Components;
using SharedModels;

namespace CreditDocumentFrontend.Pages.Components.NewCase;

public partial class AddNewCaseStep2
{
    [CascadingParameter] public CreditDocument CreditDocument { get; set; }
    
    [Parameter] public EventCallback CloseModal { get; set; }
    [Parameter] public EventCallback GoToStep3 { get; set; }
    
    protected DocumentAttachment latestAttachment;
    
    protected async Task OnFileChanged(FileChangedEventArgs e)
    {
        foreach (var file in e.Files)
        {
            // Convert the file to a byte array.
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);

            // Convert the byte array to a base64 string.
            var base64FileRepresentation = Convert.ToBase64String(buffer);

            if (CreditDocument.Attachments.Any(a => string.IsNullOrEmpty(a.FileUrl)) == true)
            {
                // Log.LogError("error");
                throw new ArgumentException("FileUrl cannot be null or empty");
                
                // Log an error, display an error message, or throw an exception
                //continue;  // Skip this file
            }

            latestAttachment = new DocumentAttachment
            {
                FileName = file.Name,
                FileUrl = $"data:{file.Type};base64,{base64FileRepresentation}",
                Status = "Uploaded"
            };

            CreditDocument.Attachments.Add(latestAttachment);
        }

        // Update the UI
        StateHasChanged();
    }
    
    private void RemoveFile(DocumentAttachment attachment)
    {
        CreditDocument.Attachments.Remove(attachment);
        StateHasChanged();
    }
    
    protected void AddItem()
    {
        if (latestAttachment != null)
        {
            var newItem = new Tuple<string, string, DocumentAttachment>(
                CreditDocument.Guarantors[0].Name,
                CreditDocument.Guarantors[0].Email,
                latestAttachment);
            // AddedItems.Add(newItem);
        }
    }

    private Person _newPerson = new();
    
    private void HandleValidSubmit()
    {
        CreditDocument.Guarantors.Add(_newPerson);
        _newPerson = new();
    }
    
    private void CloseModalNow()
    {
        CloseModal.InvokeAsync();
    }

    private void OnClickNext()
    {
        GoToStep3.InvokeAsync();
    }
}