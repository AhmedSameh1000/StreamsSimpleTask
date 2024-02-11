using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;

namespace SimpleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentFileController : ControllerBase
    {
        private readonly IDocumentFileService _DocumentFileService;

        public DocumentFileController(IDocumentFileService documentFileService)
        {
            _DocumentFileService = documentFileService;
        }

        [HttpDelete("DeleteDocumentFile")]
        public async Task<IActionResult> DeleteDocumentFile(int id)
        {
            var Result = await _DocumentFileService.DeleteDocumentFile(id);

            if (!Result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("GetFileBuDocumentid")]
        public async Task<IActionResult> GetFilesByDocumentid(int DocumentId)
        {
            var Result = await _DocumentFileService.GetFilesByDocumentId(DocumentId);

            if (Result is null)
                return BadRequest();

            return Ok(Result);
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            var filePath = await _DocumentFileService.GetFilePath(fileId);
            var filename = Path.GetFileName(filePath);
            if (filePath is null)
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var name = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", name);
        }

        [HttpPost("InsertFileIntoDocument")]
        public async Task<IActionResult> InsertFileIntoDocument([FromForm] FileForCreateDTO FileModel)
        {
            var Result = await _DocumentFileService.InsertFilesIntoDocument(FileModel);
            if (!Result)
                return BadRequest();

            return Ok(Result);
        }
    }
}