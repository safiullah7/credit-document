// CreditDocumentBackend/Repositories/Repository.cs
using System.Collections.Generic;
using System.Linq;
using CreditDocumentBackend.Data;
using CreditDocumentBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedModels;

namespace CreditDocumentBackend.Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CreditDocument> GetCreditDocuments()
        {
            return _context.CreditDocuments.Include(c => c.Guarantors).Include(c => c.Signatories).Include(c => c.Attachments).ToList();
        }

        public CreditDocument GetCreditDocumentById(int id)
        {
            return _context.CreditDocuments.Include(c => c.Guarantors).Include(c => c.Signatories).Include(c => c.Attachments).FirstOrDefault(c => c.Id == id);
        }

        public CreditDocument CreateCreditDocument(CreditDocument creditDocument)
        {
            _context.CreditDocuments.Add(creditDocument);
            _context.SaveChanges();
            return creditDocument;
        }

        public CreditDocument UpdateCreditDocument(CreditDocument creditDocument)
        {
            _context.Entry(creditDocument).State = EntityState.Modified;
            _context.SaveChanges();
            return creditDocument;
        }

        public void DeleteCreditDocument(int id)
        {
            var creditDocument = _context.CreditDocuments.Find(id);
            if (creditDocument != null)
            {
                _context.CreditDocuments.Remove(creditDocument);
                _context.SaveChanges();
            }
        }
    }
}
