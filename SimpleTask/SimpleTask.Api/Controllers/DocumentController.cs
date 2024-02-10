using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;

namespace SimpleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _DocumentService;

        public DocumentController(IDocumentService documentService)
        {
            _DocumentService = documentService;
        }

        [HttpPost("CreateDocument")]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentForCreateDTo DocumentModel)
        {
            var Result = await _DocumentService.CreateDocumentAsync(DocumentModel);

            if (!Result)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }

        [HttpPut("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument([FromForm] DocumentForCreateDTo DocumentModel, int documentId)
        {
            var Result = await _DocumentService.UpdateDocumentAsync(DocumentModel, documentId);

            if (!Result)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }

        [HttpDelete("DeleteDocument")]
        public async Task<IActionResult> DeleteDocument(int documentId, string UserId)
        {
            var Result = await _DocumentService.DeleteDocument(documentId, UserId);

            if (!Result)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }

        [HttpGet("GetUserDocuments")]
        public async Task<IActionResult> GetUserDocuments(string UserId)
        {
            var Docuemnts = await _DocumentService.GetUserDocuments(UserId);
            if (Docuemnts == null)
                return BadRequest();
            return Ok(Docuemnts);
        }
    }
}