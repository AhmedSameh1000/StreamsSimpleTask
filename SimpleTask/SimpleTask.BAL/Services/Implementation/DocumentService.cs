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
        private readonly IUserRepository _UserRepository;

        public DocumentService(IDocumentRepository documentRepository,
            IFileServices fileServices,
            IWebHostEnvironment Host, IUserRepository userRepository)
        {
            _DocumentRepository = documentRepository;
            _FileServices = fileServices;
            _Host = Host;
            _UserRepository = userRepository;
        }

        public async Task<bool> CreateDocumentAsync(DocumentForCreateDTo documentModel)
        {
            if (documentModel is null)
            {
                return false;
            }

            var DocumentsFile = _FileServices.SaveModelFiles(documentModel);

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
                var DocumentsFile = _FileServices.SaveModelFiles(documentModel);
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

        public async Task<List<DocumentForReturnDTO>> GetUserDocuments(string UserId)
        {
            var Documents = await _DocumentRepository.GetAllAsNoTracking(c => c.applicationUserId == UserId, new[] { "applicationUser", "priority", "documents" });

            if (Documents is null)
                return null;

            return Documents.Select(d => new DocumentForReturnDTO()
            {
                Id = d.Id,
                UserId = UserId,
                Name = d.Name,
                CreatedDate = d.Created_Date,
                DueDate = d.Due_Date,
                Priorty = d.priority.Name,
                UserEmail = d.applicationUser.Email,
                UserName = d.applicationUser.Name,
                DocumentFiles = d.documents.Select(c => Path.Combine(_Host.WebRootPath, "Documents", c.File_Path)).ToList(),
                DocumentFileCount = d.documents.Count()
            }).ToList();
        }

        public async Task<SingleDocumentForReturnDTO> GetDocumentById(int DocumentId)
        {
            var Docment = await _DocumentRepository.GetFirstOrDefault(c => c.Id == DocumentId);

            if (Docment is null)
            {
                return null;
            }
            return new SingleDocumentForReturnDTO()
            {
                Id = Docment.Id,
                DueDate = Docment.Due_Date,
                Name = Docment.Name,
                PriortyId = Docment.PriorityId
            };
        }

        public async Task<List<UserWithHisDocumentsForReturnDto>> GetUsersWithDocuments()
        {
            var users = await _UserRepository.GetAllAsNoTracking(new[] { "documents" });

            var Result = users.Where(c => c.documents.Count() != 0).Select(u => new UserWithHisDocumentsForReturnDto()
            {
                Email = u.Email,
                UserId = u.Id,
                UserName = u.Name
            }).ToList();

            return Result;
        }
    }
}