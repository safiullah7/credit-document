using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using SharedModels;

namespace CreditDocumentFrontend.Services
{
    public class CreditDocumentService
    {
        private readonly HttpClient _http;

        public CreditDocumentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CreditDocument>> GetCreditDocumentsAsync()
        {
            return await _http.GetFromJsonAsync<List<CreditDocument>>("https://localhost:7157/api/Creditdocuments");
        }

        public async Task UpdateCreditDocumentAsync(CreditDocument creditDocument)
        {
            await _http.PutAsJsonAsync($"https://localhost:7157/api/Creditdocuments/{creditDocument.Id}", creditDocument);
        }

        public async Task CreateCreditDocumentAsync(CreditDocument creditDocument)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("https://localhost:7157/api/Creditdocuments", creditDocument);

                // Ensure the request was a success
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
