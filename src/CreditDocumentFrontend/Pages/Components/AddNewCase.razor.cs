using SharedModels;
using Microsoft.AspNetCore.Components;
using SharedModels;
using CreditDocumentFrontend.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Blazorise;
using System.Net.Mail;

namespace CreditDocumentFrontend.Pages.Components
{
    public partial class AddNewCase
    {
        [Parameter] public bool IsOpen { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public EventCallback OnDialogClose { get; set; }
        [Parameter] public CreditDocument? CreditDocument { get; set; }
        [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }

        private string _selectedStep = "step1";
        //private CreditDocument _creditDocument = new();
        private FileEdit fileEdit;
        private List<IFileEntry> files = new List<IFileEntry>();

        private ElementReference fileUpload;
        private ElementReference fileInputRef;
        //private Dictionary<string, bool> selectedRegions = new Dictionary<string, bool>();
        private bool showRegionsDropdown = false;
        private Blazorise.FileEdit? fileEditRef;
        public List<DocumentAttachment> Attachments { get; set; } = new List<DocumentAttachment>();
        protected List<Tuple<string, string, DocumentAttachment>> AddedItems = new List<Tuple<string, string,  DocumentAttachment>>();
        protected DocumentAttachment latestAttachment;


        private bool showSecondCard = false;
        //private Blazorise.FileEdit secondFileEditRef;
        private List<IFileEntry> Files = new List<IFileEntry>();


        protected override void OnInitialized()
        {
            base.OnInitialized();
            CreditDocument = new CreditDocument
            {
                Guarantors = new List<Person>(),
                Signatories = new List<Person>(),
                Attachments = new List<DocumentAttachment>(),
                //State = DocumentState.OnHold
            };

            CreditDocument.Guarantors.Add(new Person());
            CreditDocument.Signatories.Add(new Person());

        }
        private async Task CloseModal()
        {
            IsOpen = false;
            await IsOpenChanged.InvokeAsync(IsOpen);
        }

        private async Task TriggerFileInput()
        {
            await JSRuntime.InvokeVoidAsync("triggerClick", fileInputRef);
        }
        private Task OnSelectedStepChanged(string name)
        {
            _selectedStep = name;

            return Task.CompletedTask;
        }

        private async Task GoToStep2()
        {
            _selectedStep = "step2";
            Console.WriteLine(CreditDocument?.LoanNumber + " is LoanNumber."); // Check values before sending
            Console.WriteLine(CreditDocument?.CompanyName + " is CompanyName.");
            Console.WriteLine(CreditDocument?.OrganizationNumber + " is OrganizationNumber."); // Check values before sending
            Console.WriteLine(CreditDocument?.Guarantors + " is Guarantors.");
            Console.WriteLine(CreditDocument?.Signatories + " is Signatories.");
            if (CreditDocument == null || CreditDocumentService == null)
            {
                Log.LogError("CreditDocument or CreditDocumentService is null.");
                //Console.WriteLine("CreditDocument or CreditDocumentService is null.");
                return;
            }
            CreditDocument.Date = DateTime.Now;
            //CreditDocument.State = DocumentState.OnHold; // Updating the state

            await CreditDocumentService.CreateCreditDocumentAsync(CreditDocument);
            IsOpen = false;
            await OnSave.InvokeAsync(CreditDocument);
            await OnDialogClose.InvokeAsync();
            //await CloseModal();
        }

        private void GoToStep3()
        {
            _selectedStep = "step3";
        }
        
        private async Task HandleFileUpload(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                // Convert the file to a byte array.
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);

                // Convert the byte array to a base64 string.
                var base64FileRepresentation = Convert.ToBase64String(buffer);

                // Check that the file read was successful and that all fields are populated
                if (CreditDocument?.Attachments.Any(a => string.IsNullOrEmpty(a.FileUrl)) == true)
                {
                    Log.LogError("error");
                    throw new ArgumentException("FileUrl cannot be null or empty");
                    // Log an error, display an error message, or throw an exception
                    //continue;  // Skip this file
                }

                CreditDocument?.Attachments.Add(new DocumentAttachment
                {
                    FileName = file.Name,
                    FileUrl = $"data:{file.ContentType};base64,{base64FileRepresentation}",
                    Status = "Uploaded"
                });
            }
            // Update the UI
            StateHasChanged();
        }
        //private void HandleRemoveAttachment(DocumentAttachment attachment)
        //{
        //    CreditDocument?.Attachments.Remove(attachment);
        //}

        private async Task  pOnFileChanged(FileChangedEventArgs e)
        {
            foreach (var file in e.Files)
            {
                // Convert the file to a byte array.
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);

                // Convert the byte array to a base64 string.
                var base64FileRepresentation = Convert.ToBase64String(buffer);

                if (Attachments.Any(a => string.IsNullOrEmpty(a.FileUrl)) == true)
                {
                    Log.LogError("error");
                    throw new ArgumentException("FileUrl cannot be null or empty");
                    // Log an error, display an error message, or throw an exception
                    //continue;  // Skip this file
                }

                Attachments.Add(new DocumentAttachment
                {
                    FileName = file.Name,
                    FileUrl = $"data:{file.Type};base64,{base64FileRepresentation}",
                    Status = "Uploaded"
                });
            }

            // Update the UI
            StateHasChanged();
        }

        

        
    }

}

