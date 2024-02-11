using Microsoft.AspNetCore.Http;
using SimpleTask.BAL.DTOs;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IFileServices
    {
        void DeleteFile(string Folderpath, string fileNamewithExtension);

        FileInformation SaveFile(IFormFile file, string FolderPath);
    }
}