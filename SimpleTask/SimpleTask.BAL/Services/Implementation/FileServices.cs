using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;

namespace SimpleTask.BAL.Services.Implementation
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment _Host;

        public FileServices(IWebHostEnvironment Host)
        {
            _Host = Host;
        }

        public void DeleteFile(string Folderpath, string fileNamewithExtension)
        {
            var path = Path.Combine(_Host.WebRootPath, Folderpath, Path.GetFileName(fileNamewithExtension));
            var IsExist = Path.Exists(path);
            if (IsExist)
            {
                File.Delete(path);
            }
        }

        public FileInformation SaveFile(IFormFile file, string FolderPath)
        {
            var FileUrl = "";
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            using (FileStream fileStreams = new(Path.Combine(FolderPath,
                            fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            FileUrl = fileName + extension;
            return new FileInformation()
            {
                Path = FileUrl,
                Name = file.FileName
            };
        }
    }
}