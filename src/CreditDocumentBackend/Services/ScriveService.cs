// CreditDocumentBackend/Services/ScriveService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CreditDocumentBackend.Interfaces;
using Microsoft.Extensions.Configuration;
using SharedModels;

namespace CreditDocumentBackend.Services
{
    public class ScriveService : IScriveService
    {
        private readonly HttpClient _httpClient;
        private readonly string _scriveApiUrl;

        public ScriveService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _scriveApiUrl = configuration.GetValue<string>("ScriveApiUrl");
        }

        public async Task<IEnumerable<CreditDocument>> GetDocumentsAsync()
        {
            // Implement the logic to call the Scrive API and return the list of documents
            throw new NotImplementedException();
        }

        public async Task<CreditDocument> GetDocumentAsync(string documentId)
        {
            // Implement the logic to call the Scrive API and return the specific document by ID
            throw new NotImplementedException();
        }

        public async Task<CreditDocument> CreateDocumentAsync(CreditDocument document)
        {
            // Implement the logic to call the Scrive API to create a new document
            throw new NotImplementedException();
        }

        public async Task UpdateDocumentStatusAsync(string documentId, string status)
        {
            // Implement the logic to call the Scrive API to update the status of a document
            throw new NotImplementedException();
        }
    }
}
