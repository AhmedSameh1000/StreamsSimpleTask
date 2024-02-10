using Microsoft.AspNetCore.Hosting;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.BAL.Services.Implementation
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _DocumentRepository;
        private readonly IFileServices _FileServices;
        private readonly IWebHostEnvironment _Host;

        public DocumentService(IDocumentRepository documentRepository,
            IFileServices fileServices,
            IWebHostEnvironment Host)
        {
            _DocumentRepository = documentRepository;
            _FileServices = fileServices;
            _Host = Host;
        }

        public async Task<bool> CreateDocumentAsync(DocumentForCreateDTo documentModel)
        {
            var DocumentsFile = SaveModelFiles(documentModel);

            var Document = new Document()
            {
                Created_Date = DateTime.Now,
                Due_Date = documentModel.Due_date.Date,
                Name = documentModel.Name,
                PriorityId = documentModel.PriorityId,
                documents = DocumentsFile,
                applicationUserId = documentModel.UserId
            };
            await _DocumentRepository.Add(Document);
            return await _DocumentRepository.SaveChanges();
        }

        public async Task<bool> UpdateDocumentAsync(DocumentForCreateDTo documentModel, int documentId)
        {
            var Document = await _DocumentRepository.GetFirstOrDefault(
                c => c.Id == documentId
                && c.applicationUserId == documentModel.UserId,
                new[] { "documents" });

            if (Document is null)
                return false;

            if (documentModel.files != null)
            {
                //1--Delete Current Files

                if (Document.documents != null)
                {
                    Document.documents.ForEach(file =>
                    {
                        _FileServices.DeleteFile("Documents", file.File_Path);
                    });
                }

                //save new files
                var DocumentsFile = SaveModelFiles(documentModel);
                Document.documents = DocumentsFile;
            }

            Document.PriorityId = documentModel.PriorityId;
            Document.Name = documentModel.Name;
            Document.Due_Date = documentModel.Due_date;

            _DocumentRepository.Update(Document);

            return await _DocumentRepository.SaveChanges();
        }

        public async Task<bool> DeleteDocument(int DocumentId, string UserId)
        {
            var Document = await _DocumentRepository.GetFirstOrDefault(
                c => c.Id == DocumentId
                && c.applicationUserId == UserId
                , new[] { "documents" });

            if (Document is null)
                return false;

            var DocumentFilePathes = Document.documents.Select(c => c.File_Path).ToList();

            _DocumentRepository.Remove(Document);
            var Result = await _DocumentRepository.SaveChanges();

            if (Result && DocumentFilePathes != null)
            {
                DocumentFilePathes.ForEach(filePath =>
                {
                    if (filePath != null)
                        _FileServices.DeleteFile("Documents", filePath);
                });
            }

            return Result;
        }

        private List<DocumentFile> SaveModelFiles(DocumentForCreateDTo documentModel)
        {
            var DocumentPath = Path.Combine(_Host.WebRootPath, "Documents");
            var DocumentsFile = new List<DocumentFile>();
            documentModel.files.ForEach(f =>
            {
                if (!Path.Exists(DocumentPath))
                {
                    Directory.CreateDirectory(DocumentPath);
                }
                var Url = _FileServices.SaveFile(f, DocumentPath);
                DocumentsFile.Add(new DocumentFile()
                {
                    File_Path = Url,
                });
            });

            return DocumentsFile;
        }
    }
}