using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.BAL.Services.Implementation
{
    public class DocumentFileService : IDocumentFileService
    {
        private readonly IDocumentFileRepository _DocumentFileRepository;
        private readonly IFileServices _FileServices;
        private readonly IWebHostEnvironment _Host;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IDocumentRepository _DocumentRepository;
        private readonly IUserRepository _UserRepository;

        public DocumentFileService(
            IDocumentFileRepository documentFileRepository,
            IFileServices fileServices,
            IWebHostEnvironment Host,
            IHttpContextAccessor httpContextAccessor,
           IDocumentRepository documentRepository,
           IUserRepository userRepository
           )
        {
            _DocumentFileRepository = documentFileRepository;
            _FileServices = fileServices;
            _Host = Host;
            _HttpContextAccessor = httpContextAccessor;
            _DocumentRepository = documentRepository;
            _UserRepository = userRepository;
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

        public async Task<string> GetFilePath(int fileId)
        {
            var File = await _DocumentFileRepository.GetFirstOrDefault(c => c.Id == fileId);

            if (File is null)
            {
                return null;
            }

            var filePath = Path.Combine(_Host.WebRootPath, "Documents", File.File_Path);

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            return filePath;
        }

        public async Task<List<DocumentFileToReturn>> GetFilesByDocumentId(int DocumentId)
        {
            var DocuemntsFile = await _DocumentFileRepository.GetAllAsNoTracking(c => c.DocumentId == DocumentId);

            if (DocuemntsFile is null)
                return null;

            var Result = DocuemntsFile.Select(c => new DocumentFileToReturn()
            {
                id = c.Id,
                filePath = Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "Documents", c.File_Path),
                FileName = c.FileName
            }).ToList();
            return Result;
        }

        public async Task<List<DocumentFileToReturn>> GetUserFiles(string UserId)
        {
            var Documents = await _DocumentRepository.GetAllAsNoTracking(c => c.applicationUserId == UserId, new[] { "documents" });

            var documentFiles = Documents.SelectMany(doc => doc.documents)
                                     .Select(df => new DocumentFileToReturn
                                     {
                                         id = df.Id,
                                         filePath = Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "Documents", df.File_Path),
                                         FileName = df.FileName // assuming you have this property in DocumentFile
                                     })
                                     .ToList();

            return documentFiles;
        }

        public async Task<bool> InsertFilesIntoDocument(FileForCreateDTO fileModel)
        {
            var Document = await _DocumentRepository.GetFirstOrDefault(c => c.Id == fileModel.DocumentId, new[] { "documents" });

            if (Document is null)
            {
                return false;
            }

            fileModel.files.ForEach(file =>
            {
                var fileInformation = _FileServices.SaveFile(file, Path.Combine(_Host.WebRootPath, "Documents"));
                var DocumentFile = new DocumentFile()
                {
                    File_Path = fileInformation.Path,
                    FileName = fileInformation.Name
                };
                Document.documents.Add(DocumentFile);
            });

            _DocumentRepository.Update(Document);
            return await _DocumentRepository.SaveChanges();
        }
    }
}