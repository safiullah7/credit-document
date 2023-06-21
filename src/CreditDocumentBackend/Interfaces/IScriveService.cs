// CreditDocumentBackend/Interfaces/IScriveService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModels;

namespace CreditDocumentBackend.Interfaces
{
    public interface IScriveService
    {
        Task<IEnumerable<CreditDocument>> GetDocumentsAsync();
        Task<CreditDocument> GetDocumentAsync(string documentId);
        Task<CreditDocument> CreateDocumentAsync(CreditDocument document);
        Task UpdateDocumentStatusAsync(string documentId, string status);
    }
}
