using Microsoft.AspNetCore.Http;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IFileServices
    {
        void DeleteFile(string Folderpath, string fileNamewithExtension);

        string SaveFile(IFormFile file, string FolderPath);
    }
}