// CreditDocumentBackend/Interfaces/IRepository.cs
using System.Collections.Generic;
using SharedModels;

namespace CreditDocumentBackend.Interfaces
{
    public interface IRepository
    {
        IEnumerable<CreditDocument> GetCreditDocuments();
        CreditDocument GetCreditDocumentById(int id);
        CreditDocument CreateCreditDocument(CreditDocument creditDocument);
        CreditDocument UpdateCreditDocument(CreditDocument creditDocument);
        void DeleteCreditDocument(int id);
    }
}
