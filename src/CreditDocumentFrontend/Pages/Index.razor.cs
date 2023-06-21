using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using SharedModels;
using CreditDocumentFrontend.Services;
using System.Net.Http.Json;


namespace CreditDocumentFrontend.Pages
{
    public partial class Index
    {
        private List<CreditDocument>? creditDocuments;
        private bool editDialogIsOpen = false;
        private bool showNewCaseForm = false;
        private CreditDocument selectedCreditDocument = null;

        [Inject] public CreditDocumentService CreditDocumentService { get; set; }
  
 
        protected override async Task OnInitializedAsync()
        {
           

            creditDocuments = await CreditDocumentService.GetCreditDocumentsAsync();


        }

        //private void OpenNewCaseForm()
        //{
        //    showNewCaseForm = true;
        //}

        //private void HandleModalClosed(bool isOpen)
        //{
        //    showNewCaseForm = isOpen;
        //}
        private void OpenNewCaseForm()
        {

            showNewCaseForm = true;
        }

        private void HandleModalClosed()
        {
            showNewCaseForm = false;
        }
        private void OpenEditDialog(CreditDocument creditDocument)
        {
            selectedCreditDocument = creditDocument;
            editDialogIsOpen = true;
        }

        private void CloseEditDialog()
        {
            editDialogIsOpen = false;
        }
        //private async Task HandleNewCreditDocumentAdded(CreditDocument newDocument)
        //{
        //    await RefreshCreditDocuments();
        //}
        private async Task RefreshCreditDocuments()
        {
            creditDocuments = await CreditDocumentService.GetCreditDocumentsAsync();
        }
        private RenderFragment RenderCreditDocuments(DocumentState state) => builder =>
        {
            var documents = creditDocuments.FindAll(cd => cd.State == state);

            foreach (var document in documents)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "credit-document");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => OpenEditDialog(document)));
                builder.OpenElement(0, "div");
                builder.AddAttribute(2, "class", "Date");
                builder.AddContent(3, document.Date);
                builder.CloseElement();
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "LoanNumber");
                builder.AddContent(3, document.LoanNumber);
                builder.CloseElement();
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "CompanyName");
                builder.AddContent(2, document.CompanyName);
                builder.CloseElement();
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "Name");

                
                foreach (var signatory in document.Signatories)
                {

                    builder.AddAttribute(1, "class", "name-row");
                    builder.OpenElement(2, "span");
                    builder.AddAttribute(3, "class", "checkmark"); 
                    builder.AddContent(4, "✔︎ "); 
                    builder.CloseElement();

                    builder.AddContent(5, signatory.Name);
                 
                }
                builder.CloseElement();
                builder.CloseElement();
            }
        };
    }
}

