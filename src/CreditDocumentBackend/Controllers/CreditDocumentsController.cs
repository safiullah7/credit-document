using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CreditDocumentBackend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace CreditDocumentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditDocumentsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IScriveService _scriveService;

        public CreditDocumentsController(IRepository repository, IScriveService scriveService)
        {
            _repository = repository;
            _scriveService = scriveService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CreditDocument>> GetCreditDocuments()
        {
            return Ok(_repository.GetCreditDocuments());
        }

        [HttpGet("{id}")]
        public ActionResult<CreditDocument> GetCreditDocument(int id)
        {
            var creditDocument = _repository.GetCreditDocumentById(id);
            if (creditDocument == null)
            {
                return NotFound();
            }

            return Ok(creditDocument);
        }

        [HttpPost]
        public async Task<ActionResult<CreditDocument>> CreateCreditDocument([FromBody] CreditDocument creditDocument)
        {
            // Create the document in the local database
            var createdDocument = _repository.CreateCreditDocument(creditDocument);

            // Create the document in Scrive
            await _scriveService.CreateDocumentAsync(createdDocument);

            return CreatedAtAction(nameof(GetCreditDocument), new { id = createdDocument.Id }, createdDocument);
        }
        
        [HttpPut("{id}")]
        public ActionResult<CreditDocument> UpdateCreditDocument(int id, [FromBody] CreditDocument creditDocument)
        {
            if (id != creditDocument.Id)
            {
                return BadRequest();
            }

            var updatedDocument = _repository.UpdateCreditDocument(creditDocument);

            return Ok(updatedDocument);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCreditDocument(int id)
        {
            _repository.DeleteCreditDocument(id);
            return NoContent();
        }
    }
}