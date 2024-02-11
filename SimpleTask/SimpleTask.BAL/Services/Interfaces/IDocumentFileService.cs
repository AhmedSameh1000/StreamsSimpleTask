using SimpleTask.BAL.DTOs;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IDocumentFileService
    {
        Task<bool> DeleteDocumentFile(int DocumentFileId);

        Task<List<DocumentFileToReturn>> GetFilesByDocumentId(int DocumentId);

        Task<string> GetFilePath(int fileId);

        Task<bool> InsertFilesIntoDocument(FileForCreateDTO fileModel);
    }
}