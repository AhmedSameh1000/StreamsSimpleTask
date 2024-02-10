namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IDocumentFileService
    {
        Task<bool> DeleteDocumentFile(int DocumentFileId);
    }
}