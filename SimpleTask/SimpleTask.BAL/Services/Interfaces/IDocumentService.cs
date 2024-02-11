using SimpleTask.BAL.DTOs;
using SimpleTask.DAL.Domains;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> CreateDocumentAsync(DocumentForCreateDTo document);

        Task<bool> UpdateDocumentAsync(DocumentForCreateDTo document, int documentId);

        Task<bool> DeleteDocument(int DocumentId, string UserId);

        Task<List<DocumentForReturnDTO>> GetUserDocuments(string UserId);

        Task<SingleDocumentForReturnDTO> GetDocumentById(int DocumentId);
    }
}