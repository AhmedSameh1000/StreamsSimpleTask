using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}