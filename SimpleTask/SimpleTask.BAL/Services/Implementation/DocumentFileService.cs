using Microsoft.AspNetCore.Hosting;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.BAL.Services.Implementation
{
    public class DocumentFileService : IDocumentFileService
    {
        private readonly IDocumentFileRepository _DocumentFileRepository;
        private readonly IFileServices _FileServices;

        public DocumentFileService(
            IDocumentFileRepository documentFileRepository,
            IFileServices fileServices)
        {
            _DocumentFileRepository = documentFileRepository;
            _FileServices = fileServices;
        }

        public async Task<bool> DeleteDocumentFile(int DocumentFileId)
        {
            var DocumentFile = await _DocumentFileRepository.GetFirstOrDefault(c => c.Id == DocumentFileId);

            if (DocumentFile is null) return false;
            var DocumentFilepath = DocumentFile.File_Path;

            _DocumentFileRepository.Remove(DocumentFile);
            var Result = await _DocumentFileRepository.SaveChanges();

            if (Result && DocumentFilepath != null)
            {
                _FileServices.DeleteFile("Documents", DocumentFilepath);
            }
            return Result;
        }
    }
}